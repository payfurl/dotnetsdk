#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>DirectDebitDetails</c> (_shared/src/api.core/DTO/NewPayToAgreement.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class DirectDebitDetails
{
    public string? Bsb { get; set; }

    public string? AccountNumber { get; set; }
}
