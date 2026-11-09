#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>Refund</c> (_shared/src/api.core/Data/Transaction.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class Refund
{
    public decimal? Amount { get; set; }

    public string? Comment { get; set; }

    public string? UserId { get; set; }

    public DateTime? DateAdded { get; set; }

    public string? GatewayRefundId { get; set; }
}
