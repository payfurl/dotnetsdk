using System.Net;
using System.Text;

namespace Payfurl.Sdk.Tests;

/// <summary>
/// An HttpMessageHandler that records each request and answers from a queue, so the tests see exactly what the SDK
/// sends without a network. With nothing queued it answers 200 with <c>{}</c>.
/// </summary>
public sealed class FakeHandler : HttpMessageHandler
{
    private readonly Queue<Func<HttpRequestMessage, Task<HttpResponseMessage>>> _responses = new();

    public List<Recorded> Requests { get; } = new();

    public Recorded Last => Requests[^1];

    public FakeHandler Respond(int status, string body, string contentType = "application/json")
    {
        _responses.Enqueue(_ => Task.FromResult(new HttpResponseMessage((HttpStatusCode)status)
        {
            Content = new StringContent(body, Encoding.UTF8, contentType),
        }));
        return this;
    }

    public FakeHandler Throw(Exception exception)
    {
        _responses.Enqueue(_ => Task.FromException<HttpResponseMessage>(exception));
        return this;
    }

    public FakeHandler Hang()
    {
        _responses.Enqueue(async _ =>
        {
            await Task.Delay(Timeout.Infinite, _cancellation);
            throw new InvalidOperationException("unreachable");
        });
        return this;
    }

    private CancellationToken _cancellation;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _cancellation = cancellationToken;
        Requests.Add(new Recorded(
            request.Method.Method,
            request.RequestUri!,
            request.Headers.ToDictionary(h => h.Key, h => string.Join(",", h.Value), StringComparer.OrdinalIgnoreCase),
            request.Content == null ? null : await request.Content.ReadAsStringAsync(cancellationToken),
            request.Content?.Headers.ContentType?.MediaType));
        return _responses.Count > 0
            ? await _responses.Dequeue()(request)
            : new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{}", Encoding.UTF8, "application/json") };
    }

    public sealed record Recorded(string Method, Uri Uri, Dictionary<string, string> Headers, string? Body, string? ContentType)
    {
        public string PathAndQuery => Uri.PathAndQuery;
    }
}

internal static class Fake
{
    public static (PayfurlClient Client, FakeHandler Handler) Client(
        string key = "sectest123-au",
        PayfurlEnvironment environment = PayfurlEnvironment.Sandbox,
        Action<PayfurlClientOptions>? configure = null)
    {
        var handler = new FakeHandler();
        var options = new PayfurlClientOptions { SecretKey = key, Environment = environment, RetryDelay = TimeSpan.Zero };
        configure?.Invoke(options);
        return (new PayfurlClient(options, new HttpClient(handler)), handler);
    }
}
