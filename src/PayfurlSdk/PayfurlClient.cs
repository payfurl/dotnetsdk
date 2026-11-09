using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;
using Payfurl.Sdk.Resources;

namespace Payfurl.Sdk;

/// <summary>
/// The payFURL API with one account's key:
/// <code>
/// var payfurl = new PayfurlClient(new PayfurlClientOptions { SecretKey = key, Environment = PayfurlEnvironment.Sandbox });
/// var charge = await payfurl.Charges.CreateWithTokenAsync(new NewChargeToken { Amount = 20m, Currency = "AUD", Token = token });
/// </code>
/// One client per account or region; it holds no other state, so keep it for the life of the app (a singleton, or
/// a typed client with <c>services.AddHttpClient&lt;PayfurlClient&gt;()</c>).
/// </summary>
public sealed class PayfurlClient
{
    // The HttpClient used when none is given: one per process, as HttpClient is meant to be used.
    private static readonly Lazy<HttpClient> SharedHttpClient = new(() => new HttpClient(CreateHandler(acceptAnyCertificate: false)) { Timeout = Timeout.InfiniteTimeSpan });

    // The local API's development certificate is not trusted, so the Local environment accepts any certificate.
    private static readonly Lazy<HttpClient> LocalHttpClient = new(() => new HttpClient(CreateHandler(acceptAnyCertificate: true)) { Timeout = Timeout.InfiniteTimeSpan });

    private readonly ApiClient _api;

    /// <summary>Creates a client for the account and environment in <paramref name="options"/>.</summary>
    /// <param name="options">The key, the environment, and how requests are sent.</param>
    /// <param name="httpClient">
    /// Sends the requests, such as a typed client from <c>IHttpClientFactory</c> or one over a fake handler in tests.
    /// Its <c>BaseAddress</c> is not used. Without one, a client shared by the process is used.
    /// </param>
    /// <exception cref="ArgumentException">For an empty key or base URL.</exception>
    public PayfurlClient(PayfurlClientOptions options, HttpClient? httpClient = null)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (string.IsNullOrWhiteSpace(options.SecretKey))
        {
            throw new ArgumentException("The payFURL key is empty", nameof(options));
        }

        var baseUrl = (options.BaseUrl ?? BaseUrl(options.Environment, Region(options.SecretKey))).TrimEnd('/');
        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var uri) || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException($"The payFURL base URL \"{baseUrl}\" is not an http or https URL", nameof(options));
        }

        httpClient ??= options.Environment == PayfurlEnvironment.Local ? LocalHttpClient.Value : SharedHttpClient.Value;
        _api = new ApiClient(httpClient, options.SecretKey.Trim(), baseUrl, options);
        Batches = new Batches(_api);
        Charges = new Charges(_api);
        Customers = new Customers(_api);
        Disputes = new Disputes(_api);
        Info = new Info(_api);
        PaymentLinks = new PaymentLinks(_api);
        PaymentMethods = new PaymentMethods(_api);
        Providers = new Providers(_api);
        Settlements = new Settlements(_api);
        Subscriptions = new Subscriptions(_api);
        Tokens = new Tokens(_api);
        Transfers = new Transfers(_api);
        Vaults = new Vaults(_api);
        WebhookSubscriptions = new WebhookSubscriptions(_api);
    }

    /// <summary>This SDK's version, as sent in the User-Agent of every request (<c>payfurl-dotnet/3.0.0</c>).</summary>
    public static string Version => ApiClient.Version;

    /// <summary>Batches of charges.</summary>
    public Batches Batches { get; }

    /// <summary>Charges: create, get, search, refund, capture, void.</summary>
    public Charges Charges { get; }

    /// <summary>Customers and their payment methods.</summary>
    public Customers Customers { get; }

    /// <summary>Disputes.</summary>
    public Disputes Disputes { get; }

    /// <summary>What a checkout can show (<c>/info/providers</c>).</summary>
    public Info Info { get; }

    /// <summary>Payment links.</summary>
    public PaymentLinks PaymentLinks { get; }

    /// <summary>Payment methods without a customer.</summary>
    public PaymentMethods PaymentMethods { get; }

    /// <summary>The account's gateways.</summary>
    public Providers Providers { get; }

    /// <summary>Settlements.</summary>
    public Settlements Settlements { get; }

    /// <summary>Subscriptions.</summary>
    public Subscriptions Subscriptions { get; }

    /// <summary>Card tokens.</summary>
    public Tokens Tokens { get; }

    /// <summary>Transfers.</summary>
    public Transfers Transfers { get; }

    /// <summary>Vaulted cards.</summary>
    public Vaults Vaults { get; }

    /// <summary>Webhook subscriptions.</summary>
    public WebhookSubscriptions WebhookSubscriptions { get; }

    /// <summary><c>GET /</c>: whether the API is up.</summary>
    public Task<RootStatus> GetStatusAsync(CancellationToken cancellationToken = default) =>
        _api.GetAsync<RootStatus>("/", null, cancellationToken);

    /// <summary><c>GET /version</c>: the API's version and region.</summary>
    public Task<ApplicationVersion> GetVersionAsync(CancellationToken cancellationToken = default) =>
        _api.GetAsync<ApplicationVersion>("/version", null, cancellationToken);

    /// <summary>The API's base URL for an environment and a key's region (<c>au</c>), or the global host when the region has none of its own.</summary>
    internal static string BaseUrl(PayfurlEnvironment environment, string? region) => (environment, region) switch
    {
        (PayfurlEnvironment.Development, "au") => "https://develop-api-au.payfurl.com",
        (PayfurlEnvironment.Sandbox, "au") => "https://sandbox-api-au.payfurl.com",
        (PayfurlEnvironment.Production, "au") => "https://api-au.payfurl.com",
        (PayfurlEnvironment.Local, _) => "https://localhost:5001",
        (PayfurlEnvironment.Development, _) => "https://develop-api.payfurl.com",
        (PayfurlEnvironment.Sandbox, _) => "https://sandbox-api.payfurl.com",
        (PayfurlEnvironment.Production, _) => "https://api.payfurl.com",
        _ => throw new ArgumentOutOfRangeException(nameof(environment), environment, "Unknown payFURL environment"),
    };

    /// <summary>A key ends in its account's region, such as <c>sectest...-au</c> (AuthService.GenerateKey).</summary>
    internal static string? Region(string key)
    {
        var dash = key.LastIndexOf('-');
        return dash < 0 ? null : key.Substring(dash + 1).Trim().ToLowerInvariant();
    }

    private static HttpMessageHandler CreateHandler(bool acceptAnyCertificate)
    {
#if NET
        var handler = new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(5) };
        if (acceptAnyCertificate)
        {
            handler.SslOptions.RemoteCertificateValidationCallback = (_, _, _, _) => true;
        }

        return handler;
#else
        var handler = new HttpClientHandler();
        if (acceptAnyCertificate)
        {
            handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
        }

        return handler;
#endif
    }
}
