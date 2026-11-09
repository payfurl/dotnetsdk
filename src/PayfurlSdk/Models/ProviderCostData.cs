#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ProviderCostDataModel</c> (api/src/api/Models/ProviderData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class ProviderCostData
{
    public bool? IsInternationalCard { get; set; }

    public string? CardScheme { get; set; }

    public decimal? TransactionCost { get; set; }

    public decimal? TransactionPercentage { get; set; }
}
