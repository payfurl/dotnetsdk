#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ProviderVisibilityDataModel</c> (api/src/api/Models/ProviderData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class ProviderVisibilityData
{
    public string? Currency { get; set; }

    public decimal? LessThanAmount { get; set; }

    public decimal? GreaterThanAmount { get; set; }

    public bool? ApplyCurrencyConversion { get; set; }
}
