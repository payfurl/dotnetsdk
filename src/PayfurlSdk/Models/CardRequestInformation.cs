#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>CardRequestInformation</c> (api/src/api/Models/CardRequestInformation.cs), in a request.
/// </summary>
public sealed class CardRequestInformation
{
    public required string CardNumber { get; set; }

    public required string ExpiryDate { get; set; }

    public string? Ccv { get; set; }

    public string? Cardholder { get; set; }

    public string? ThreeDSServerTransID { get; set; }

    public Dictionary<string, string>? ExternalThreeDsData { get; set; }
}
