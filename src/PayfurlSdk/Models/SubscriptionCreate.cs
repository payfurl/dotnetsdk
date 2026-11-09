#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>SubscriptionCreate</c> (api/src/api/Models/Subscriptions/SubscriptionCreate.cs), in a request.
/// </summary>
public sealed class SubscriptionCreate
{
    public required string PaymentMethodId { get; set; }

    public required decimal Amount { get; set; }

    public required string Currency { get; set; }

    public required SubscriptionInterval Interval { get; set; }

    public required int Frequency { get; set; }

    public DateTime? StartDate { get; set; }

    public SubscriptionEnd? EndAfter { get; set; }

    public SubscriptionRetryPolicy? Retry { get; set; }

    public WebhookConfig? Webhook { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }
}
