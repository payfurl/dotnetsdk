#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewWebhookSubscription</c> (api/src/api/Models/NewWebhookSubscription.cs), in a request.
/// </summary>
public sealed class NewWebhookSubscription
{
    public required string Url { get; set; }

    public required List<WebhookType> Types { get; set; }

    public string? Authorization { get; set; }
}
