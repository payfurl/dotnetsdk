#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;
using Payfurl.Sdk.Internal;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The query of GET /charge (ChargeController.Search). Every filter is optional; <c>limit</c> and <c>skip</c> page through the results.
/// </summary>
public sealed class ChargeSearch
{
    public string? Reference { get; set; }

    public string? ProviderId { get; set; }

    public decimal? AmountGreaterThan { get; set; }

    public decimal? AmountLessThan { get; set; }

    public string? CustomerId { get; set; }

    public TransactionStatus? Status { get; set; }

    public DateTime? AddedAfter { get; set; }

    public DateTime? AddedBefore { get; set; }

    public string? PaymentMethodId { get; set; }

    public PaymentType? PaymentType { get; set; }

    public string? CardType { get; set; }

    public string? Currency { get; set; }

    public string? CardNumber { get; set; }

    public string? Cardholder { get; set; }

    public string? BatchId { get; set; }

    public string? SubscriptionId { get; set; }

    public string? SortBy { get; set; }

    public string? SortOrder { get; set; }

    public int? Limit { get; set; }

    public int? Skip { get; set; }

    internal IEnumerable<KeyValuePair<string, string>> ToQuery() => Query.Of(
        ("reference", Reference),
        ("providerId", ProviderId),
        ("amountGreaterThan", AmountGreaterThan),
        ("amountLessThan", AmountLessThan),
        ("customerId", CustomerId),
        ("status", Status),
        ("addedAfter", AddedAfter),
        ("addedBefore", AddedBefore),
        ("paymentMethodId", PaymentMethodId),
        ("paymentType", PaymentType),
        ("cardType", CardType),
        ("currency", Currency),
        ("cardNumber", CardNumber),
        ("cardholder", Cardholder),
        ("batchId", BatchId),
        ("subscriptionId", SubscriptionId),
        ("sortBy", SortBy),
        ("sortOrder", SortOrder),
        ("limit", Limit),
        ("skip", Skip));
}
