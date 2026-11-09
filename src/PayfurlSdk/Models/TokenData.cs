#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>TokenData</c> (api/src/api/Models/TokenData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class TokenData
{
    public string? TokenId { get; set; }

    public string? AccountId { get; set; }

    public CardData? Card { get; set; }

    public ProviderSummary? Provider { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUsed { get; set; }

    public string? PayToStatus { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }

    public ClickToPayData? ClickToPay { get; set; }
}
