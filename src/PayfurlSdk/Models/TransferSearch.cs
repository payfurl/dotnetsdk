#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;
using Payfurl.Sdk.Internal;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The query of GET /transfer (TransferController.Search). Every filter is optional; <c>limit</c> and <c>skip</c> page through the results.
/// </summary>
public sealed class TransferSearch
{
    public string? Reference { get; set; }

    public string? ProviderId { get; set; }

    public string? Status { get; set; }

    public DateTime? AddedAfter { get; set; }

    public DateTime? AddedBefore { get; set; }

    public string? SortBy { get; set; }

    public int? Limit { get; set; }

    public int? Skip { get; set; }

    internal IEnumerable<KeyValuePair<string, string>> ToQuery() => Query.Of(
        ("reference", Reference),
        ("providerId", ProviderId),
        ("status", Status),
        ("addedAfter", AddedAfter),
        ("addedBefore", AddedBefore),
        ("sortBy", SortBy),
        ("limit", Limit),
        ("skip", Skip));
}
