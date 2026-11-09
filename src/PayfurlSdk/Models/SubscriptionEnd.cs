#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>SubscriptionEnd</c> (api/src/api/Models/Subscriptions/SubscriptionEnd.cs), in a request and response.
/// </summary>
public sealed class SubscriptionEnd
{
    public decimal? Amount { get; set; }

    public int? Count { get; set; }

    public DateTime? Date { get; set; }
}
