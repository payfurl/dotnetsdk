#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>TransferData</c> (api/src/api/Models/TransferData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class TransferData
{
    public string? TransferId { get; set; }

    public string? Status { get; set; }

    public string? Reference { get; set; }

    public string? GroupReference { get; set; }

    public DateTime? DateAdded { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? ProviderId { get; set; }

    public decimal? Amount { get; set; }

    public string? Currency { get; set; }

    public string? Account { get; set; }

    public string? ChargeId { get; set; }
}
