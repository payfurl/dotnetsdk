#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ClickToPayCardData</c> (api/src/api/Models/ClickToPayCardData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class ClickToPayCardData
{
    public string? PanBin { get; set; }

    public string? PanLastFour { get; set; }

    public string? Scheme { get; set; }

    public string? PaymentCardType { get; set; }
}
