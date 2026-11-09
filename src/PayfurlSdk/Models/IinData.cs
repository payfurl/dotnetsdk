#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>IinData</c> (_shared/src/api.core/DTO/IinData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class IinData
{
    public string? Scheme { get; set; }

    public string? Country { get; set; }

    public string? Issuer { get; set; }

    public string? CountryCode { get; set; }

    public string? CardType { get; set; }
}
