using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Payfurl.Sdk.Internal;

/// <summary>
/// Sends one API call: the URL, the key and User-Agent headers, JSON in and out, retries for GETs, and the API's
/// errors as exceptions.
/// </summary>
internal sealed class ApiClient
{
    /// <summary>This SDK's version, without the commit SourceLink appends (3.0.0+abc123).</summary>
    public static readonly string Version =
        typeof(ApiClient).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+')[0]
        ?? typeof(ApiClient).Assembly.GetName().Version?.ToString(3)
        ?? "0.0.0";

    private static readonly string UserAgent = "payfurl-dotnet/" + Version;

    private readonly HttpClient _http;
    private readonly string _key;
    private readonly string _baseUrl;
    private readonly PayfurlClientOptions _options;

    public ApiClient(HttpClient http, string key, string baseUrl, PayfurlClientOptions options)
    {
        _http = http;
        _key = key;
        _baseUrl = baseUrl;
        _options = options;
    }

    public Task<T> GetAsync<T>(string path, IEnumerable<KeyValuePair<string, string>>? query, CancellationToken cancellationToken, IDictionary<string, string>? headers = null) =>
        SendAsync<T>(HttpMethod.Get, path, query, null, headers, cancellationToken);

    public Task<T> PostAsync<T>(string path, object? body, CancellationToken cancellationToken) =>
        SendAsync<T>(HttpMethod.Post, path, null, body ?? new object(), null, cancellationToken);

    public Task<T> PutAsync<T>(string path, object body, CancellationToken cancellationToken) =>
        SendAsync<T>(HttpMethod.Put, path, null, body, null, cancellationToken);

    public Task<T> DeleteAsync<T>(string path, IEnumerable<KeyValuePair<string, string>>? query, CancellationToken cancellationToken) =>
        SendAsync<T>(HttpMethod.Delete, path, query, null, null, cancellationToken);

    /// <summary>A resource ID as a path segment.</summary>
    /// <exception cref="ArgumentException">For an empty ID, which would call another endpoint (GET /charge/ is a search).</exception>
    public static string Id(string id, string name)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException(name + " is empty", name);
        }

        return Uri.EscapeDataString(id);
    }

    private async Task<T> SendAsync<T>(
        HttpMethod method,
        string path,
        IEnumerable<KeyValuePair<string, string>>? query,
        object? body,
        IDictionary<string, string>? headers,
        CancellationToken cancellationToken)
    {
        var url = _baseUrl + path + Query.ToString(query);
        var json = body == null ? null : JsonSerializer.Serialize(body, body.GetType(), Json.Options);

        // Only reads are retried: the API has no idempotency keys, so a second charge POST could charge twice.
        var retries = method == HttpMethod.Get ? Math.Max(0, _options.MaxRetries) : 0;
        for (var attempt = 0; ; attempt++)
        {
            try
            {
                return await SendOnceAsync<T>(method, url, json, headers, cancellationToken).ConfigureAwait(false);
            }
            catch (PayfurlException e) when (attempt < retries && (e is PayfurlConnectionException || e is ApiException { IsRetryable: true }))
            {
                var delay = TimeSpan.FromTicks(_options.RetryDelay.Ticks * (1L << attempt));
                if (delay > TimeSpan.Zero)
                {
                    await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }

    private async Task<T> SendOnceAsync<T>(HttpMethod method, string url, string? json, IDictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(method, url);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.TryAddWithoutValidation("User-Agent", UserAgent);
        // A public key authenticates only the endpoints that take one, such as /info/providers.
        request.Headers.Add(_key.StartsWith("pub", StringComparison.Ordinal) ? "x-publickey" : "x-secretkey", _key);
        foreach (var header in headers ?? new Dictionary<string, string>())
        {
            request.Headers.Add(header.Key, header.Value);
        }

        if (json != null)
        {
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(_options.Timeout);

        HttpResponseMessage response;
        string text;
        try
        {
            response = await _http.SendAsync(request, timeout.Token).ConfigureAwait(false);
            text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        catch (OperationCanceledException e) when (!cancellationToken.IsCancellationRequested)
        {
            // The timeout above, or the HttpClient's own.
            throw new PayfurlTimeoutException($"The payFURL API did not answer {method} {Path(url)} within {_options.Timeout.TotalSeconds:0.###} seconds", e);
        }
        catch (HttpRequestException e)
        {
            throw new PayfurlConnectionException($"Could not reach the payFURL API for {method} {Path(url)}: {e.Message}", e);
        }

        using (response)
        {
            var status = (int)response.StatusCode;
            if (status < 200 || status > 299)
            {
                throw Error(status, text);
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ApiException($"The payFURL API answered HTTP {status} with an empty body", status);
            }

            try
            {
                return JsonSerializer.Deserialize<T>(text, Json.Options)
                    ?? throw new ApiException($"The payFURL API answered HTTP {status} with null", status);
            }
            catch (JsonException e)
            {
                throw new ApiException($"The payFURL API answered HTTP {status} with a body that is not the expected JSON: {e.Message}", status, innerException: e);
            }
        }
    }

    private static string Path(string url) => new Uri(url).AbsolutePath;

    /// <summary>The API's error response (services/api/src/api/Models/Error.cs) as an exception.</summary>
    internal static ApiException Error(int status, string body)
    {
        ErrorBody? error = null;
        JsonException? parseError = null;
        try
        {
            if (body.TrimStart().StartsWith("{", StringComparison.Ordinal))
            {
                error = JsonSerializer.Deserialize<ErrorBody>(body, Json.Options);
            }
        }
        catch (JsonException e)
        {
            parseError = e;
        }

        // Some endpoints answer with a plain message (BadRequest("...")), and a load balancer with HTML.
        var text = body.Trim().Trim('"');
        var message = error?.Message
            ?? (text.Length > 0 && error == null ? (text.Length > 500 ? text.Substring(0, 500) : text) : $"HTTP {status} from the payFURL API");
        var code = error?.Code ?? ErrorCode.UnknownError;
        var retryable = error?.IsRetryable ?? (status >= 500 || status == 429);

        return status switch
        {
            401 or 403 => new AuthenticationException(message, status, code, retryable, error?.Type, error?.Resource, error?.Details, error?.GatewayCode, error?.GatewayMessage, error?.ChargeId, error?.TraceId),
            404 => new NotFoundException(message, status, code, retryable, error?.Type, error?.Resource, error?.Details, error?.GatewayCode, error?.GatewayMessage, error?.ChargeId, error?.TraceId),
            400 or 422 when code == ErrorCode.ValidationError => new InvalidRequestException(message, status, code, retryable, error?.Type, error?.Resource, error?.Details, error?.GatewayCode, error?.GatewayMessage, error?.ChargeId, error?.TraceId),
            _ => new ApiException(message, status, code, retryable, error?.Type, error?.Resource, error?.Details, error?.GatewayCode, error?.GatewayMessage, error?.ChargeId, error?.TraceId, parseError),
        };
    }

    private sealed class ErrorBody
    {
        public string? Message { get; set; }

        public ErrorCode? Code { get; set; }

        public bool? IsRetryable { get; set; }

        public string? Type { get; set; }

        public string? Resource { get; set; }

        public Dictionary<string, string>? Details { get; set; }

        public string? GatewayCode { get; set; }

        public string? GatewayMessage { get; set; }

        public string? ChargeId { get; set; }

        public string? TraceId { get; set; }
    }
}
