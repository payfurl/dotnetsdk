#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;
using Payfurl.Sdk.Internal;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The query of GET /payment_link (PaymentLinkController.Search). Every filter is optional; <c>limit</c> and <c>skip</c> page through the results.
/// </summary>
public sealed class PaymentLinkSearch
{
    public DateTime? AddedAfter { get; set; }

    public DateTime? AddedBefore { get; set; }

    public int? Limit { get; set; }

    public int? Skip { get; set; }

    internal IEnumerable<KeyValuePair<string, string>> ToQuery() => Query.Of(
        ("addedAfter", AddedAfter),
        ("addedBefore", AddedBefore),
        ("limit", Limit),
        ("skip", Skip));
}
