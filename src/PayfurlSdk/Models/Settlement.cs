#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>Settlement</c> (api/src/api/Models/Settlement.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class Settlement
{
    public string? SettlementId { get; set; }

    public DateTime? SettlementDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ProviderId { get; set; }

    public string? ProviderName { get; set; }

    public string? ChargeId { get; set; }

    public decimal? Amount { get; set; }
}
