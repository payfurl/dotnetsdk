#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>SubscriptionRetryPolicy</c> (api/src/api/Models/Subscriptions/SubscriptionRetryPolicy.cs), in a request and response.
/// </summary>
public sealed class SubscriptionRetryPolicy
{
    public int? Maximum { get; set; }

    public SubscriptionRetryInterval? Interval { get; set; }

    public int? Frequency { get; set; }
}
