#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>WebhookSubscriptionSearchResults</c> (_shared/src/api.core/DTO/WebhookSubscriptionSearchResults.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class WebhookSubscriptionList
{
    public List<WebhookSubscription>? WebhookSubscriptions { get; set; }

    public int? Count { get; set; }
}
