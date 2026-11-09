#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>PaymentLinkSearchResultModel</c> (api/src/api/Models/PaymentLink/PaymentLinkSearchResultModel.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class PaymentLinkList
{
    public List<PaymentLink>? PaymentLinks { get; set; }

    public int? Count { get; set; }
}
