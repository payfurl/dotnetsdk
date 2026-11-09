#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>UpdateSubscriptionStatus</c> (api/src/api/Models/UpdateSubscriptionStatus.cs), in a request.
/// </summary>
public sealed class UpdateSubscriptionStatus
{
    public required SubscriptionStatus Status { get; set; }
}
