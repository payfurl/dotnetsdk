#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ProviderData</c> (api/src/api/Models/ProviderData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class Provider
{
    public string? ProviderId { get; set; }

    public string? Type { get; set; }

    public string? Name { get; set; }

    public string? Environment { get; set; }

    public Dictionary<string, string>? AuthenticationParameters { get; set; }

    public Dictionary<string, string>? AdditionalParameters { get; set; }

    public string? ProviderCountry { get; set; }

    public List<ProviderCostData>? CostData { get; set; }

    public List<ProviderVisibilityData>? HideConfiguration { get; set; }

    public List<ProviderVisibilityData>? RequireConfiguration { get; set; }

    public string? FallbackProviderId { get; set; }

    public string? Currency { get; set; }

    public string? MaxCapability { get; set; }

    public bool? HasPartialRefund { get; set; }

    public ProviderInfo? FallbackProvider { get; set; }

    public bool? HasThreeDsCapability { get; set; }
}
