using System;

namespace Payfurl.Sdk;

/// <summary>
/// The account and environment a <see cref="PayfurlClient"/> calls, and how it sends requests.
/// </summary>
public sealed class PayfurlClientOptions
{
    /// <summary>
    /// The account's secret key (<c>sec...</c>), which calls every endpoint. A public key (<c>pub...</c>) calls only
    /// <see cref="Resources.Info.GetProvidersAsync"/>.
    /// </summary>
    public required string SecretKey { get; set; }

    /// <summary>The environment the key belongs to.</summary>
    public required PayfurlEnvironment Environment { get; set; }

    /// <summary>
    /// The API's URL, in place of the environment's and region's, such as <c>http://api:5000</c> on a compose network.
    /// </summary>
    public string? BaseUrl { get; set; }

    /// <summary>How long one attempt of a request may take. 60 seconds by default.</summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(60);

    /// <summary>
    /// How many times a GET is retried after a connection error, a timeout or an error the API calls retryable.
    /// Other requests are never retried: the API has no idempotency keys, so a retried charge could charge twice.
    /// </summary>
    public int MaxRetries { get; set; } = 2;

    /// <summary>The wait before the first retry, doubled before each one after it. 500 milliseconds by default.</summary>
    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromMilliseconds(500);
}
