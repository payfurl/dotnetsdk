namespace Payfurl.Sdk;

/// <summary>
/// The payFURL environment a <see cref="PayfurlClient"/> calls. The key's region (its last part, such as <c>-au</c>)
/// picks the regional host; <see cref="PayfurlClientOptions.BaseUrl"/> overrides both.
/// </summary>
public enum PayfurlEnvironment
{
    /// <summary>The API on this machine (https://localhost:5001), whose development certificate is accepted.</summary>
    Local,

    /// <summary>https://develop-api.payfurl.com, or https://develop-api-au.payfurl.com for an <c>-au</c> key.</summary>
    Development,

    /// <summary>https://sandbox-api.payfurl.com, or https://sandbox-api-au.payfurl.com for an <c>-au</c> key.</summary>
    Sandbox,

    /// <summary>https://api.payfurl.com, or https://api-au.payfurl.com for an <c>-au</c> key.</summary>
    Production,
}
