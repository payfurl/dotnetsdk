#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ClickToPayData</c> (api/src/api/Models/ClickToPayData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class ClickToPayData
{
    public Address? BillingAddress { get; set; }

    public ClickToPayCardData? CardData { get; set; }
}
