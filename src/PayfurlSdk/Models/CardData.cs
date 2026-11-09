#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>CardData</c> (_shared/src/common/Models/CardData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class CardData
{
    public string? CardNumber { get; set; }

    public string? ExpiryDate { get; set; }

    public string? Cardholder { get; set; }

    public string? Type { get; set; }

    public string? CardType { get; set; }

    public string? CardIin { get; set; }

    public bool? CcvCheck { get; set; }

    public string? ThreeDSServerTransID { get; set; }

    public IinData? IinData { get; set; }
}
