#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>SubscriptionList</c> (api/src/api/Controllers/SubscriptionController.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class SubscriptionList
{
    public int? Count { get; set; }

    public List<Subscription>? Subscriptions { get; set; }
}
