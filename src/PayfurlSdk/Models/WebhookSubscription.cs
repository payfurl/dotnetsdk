#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>WebhookSubscriptionData</c> (_shared/src/api.core/DTO/WebhookSubscriptionData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class WebhookSubscription
{
    public string? WebhookSubscriptionId { get; set; }

    public string? AccountId { get; set; }

    public string? Url { get; set; }

    public string? Authorization { get; set; }

    public List<WebhookType>? Types { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? DateRemoved { get; set; }
}
