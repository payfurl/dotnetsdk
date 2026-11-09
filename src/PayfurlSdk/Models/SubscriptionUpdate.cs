#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>SubscriptionUpdate</c> (api/src/api/Models/Subscriptions/SubscriptionUpdate.cs), in a request.
/// </summary>
public sealed class SubscriptionUpdate
{
    public required decimal Amount { get; set; }

    public required string Currency { get; set; }

    public required SubscriptionInterval Interval { get; set; }

    public required int Frequency { get; set; }

    public SubscriptionEnd? EndAfter { get; set; }

    public SubscriptionRetryPolicy? Retry { get; set; }

    public SubscriptionWebhookUpdate? Webhook { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }
}
