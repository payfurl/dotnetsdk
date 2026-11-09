#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>Subscription</c> (api/src/api/Models/Subscriptions/Subscription.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class Subscription
{
    public string? SubscriptionId { get; set; }

    public string? PaymentMethodId { get; set; }

    public decimal? Amount { get; set; }

    public string? Currency { get; set; }

    public SubscriptionInterval? Interval { get; set; }

    public int? Frequency { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? StartDate { get; set; }

    public SubscriptionEnd? EndAfter { get; set; }

    public SubscriptionRetryPolicy? Retry { get; set; }

    public WebhookConfig? Webhook { get; set; }

    public SubscriptionStatus? Status { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }
}
