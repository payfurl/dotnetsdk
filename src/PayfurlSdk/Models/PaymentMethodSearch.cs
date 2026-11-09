#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;
using Payfurl.Sdk.Internal;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The query of GET /payment_method (PaymentMethodController.Search). Every filter is optional; <c>limit</c> and <c>skip</c> page through the results.
/// </summary>
public sealed class PaymentMethodSearch
{
    public DateTime? AddedAfter { get; set; }

    public DateTime? AddedBefore { get; set; }

    public string? ProviderId { get; set; }

    public string? CustomerId { get; set; }

    public PaymentType? PaymentType { get; set; }

    public string? CardType { get; set; }

    public string? Search { get; set; }

    public string? SortBy { get; set; }

    public string? SortOrder { get; set; }

    public bool? IncludeRemoved { get; set; }

    public int? Limit { get; set; }

    public int? Skip { get; set; }

    internal IEnumerable<KeyValuePair<string, string>> ToQuery() => Query.Of(
        ("addedAfter", AddedAfter),
        ("addedBefore", AddedBefore),
        ("providerId", ProviderId),
        ("customerId", CustomerId),
        ("paymentType", PaymentType),
        ("cardType", CardType),
        ("search", Search),
        ("sortBy", SortBy),
        ("sortOrder", SortOrder),
        ("includeRemoved", IncludeRemoved),
        ("limit", Limit),
        ("skip", Skip));
}
