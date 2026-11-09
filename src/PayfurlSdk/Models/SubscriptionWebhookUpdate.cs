#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>SubscriptionWebhookUpdate</c> (api/src/api/Models/Subscriptions/SubscriptionWebhookUpdate.cs), in a request.
/// </summary>
public sealed class SubscriptionWebhookUpdate
{
    public string? Url { get; set; }

    public string? Authorization { get; set; }
}
