#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;
using Payfurl.Sdk.Internal;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The query of GET /dispute (DisputeController.Search). Every filter is optional; <c>limit</c> and <c>skip</c> page through the results.
/// </summary>
public sealed class DisputeSearch
{
    public string? TransactionId { get; set; }

    public string? ProviderId { get; set; }

    public DisputeStatus? Status { get; set; }

    public DisputeStage? Stage { get; set; }

    public string? ReasonCategory { get; set; }

    public string? Currency { get; set; }

    public decimal? AmountGreaterThan { get; set; }

    public decimal? AmountLessThan { get; set; }

    public DateTime? AddedAfter { get; set; }

    public DateTime? AddedBefore { get; set; }

    public DateTime? DueBefore { get; set; }

    public string? SortBy { get; set; }

    public string? SortOrder { get; set; }

    public int? Limit { get; set; }

    public int? Skip { get; set; }

    internal IEnumerable<KeyValuePair<string, string>> ToQuery() => Query.Of(
        ("transactionId", TransactionId),
        ("providerId", ProviderId),
        ("status", Status),
        ("stage", Stage),
        ("reasonCategory", ReasonCategory),
        ("currency", Currency),
        ("amountGreaterThan", AmountGreaterThan),
        ("amountLessThan", AmountLessThan),
        ("addedAfter", AddedAfter),
        ("addedBefore", AddedBefore),
        ("dueBefore", DueBefore),
        ("sortBy", SortBy),
        ("sortOrder", SortOrder),
        ("limit", Limit),
        ("skip", Skip));
}
