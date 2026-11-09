#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ApplePayProviderInfo</c> (_shared/src/api.core/DTO/ApplePayProviderInfo.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class ApplePayProviderInfo
{
    public string? LineItemLabel { get; set; }

    public string? MerchantIdentifier { get; set; }

    public string? DomainName { get; set; }

    public string? DisplayName { get; set; }

    public string? CountryCode { get; set; }

    public string? ProviderId { get; set; }

    public string? ProviderType { get; set; }

    public Dictionary<string, string>? ClientParameters { get; set; }
}
