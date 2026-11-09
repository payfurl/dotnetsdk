#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NetworkTokenSummary</c> (api/src/api/Models/NetworkTokenSummary.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class NetworkTokenSummary
{
    public string? NetworkTokenId { get; set; }

    public string? SchemeTokenId { get; set; }

    public string? ProviderId { get; set; }

    public string? ProviderType { get; set; }

    public string? Status { get; set; }

    public DateTime? DateAdded { get; set; }

    public Dictionary<string, string>? SchemeTokenData { get; set; }

    public NetworkTokenAuthenticationMethod? AuthenticationMethod { get; set; }
}
