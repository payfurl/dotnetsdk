#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>PayerPayIdDetails</c> (_shared/src/common/Models/PayToSummary.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class PayerPayIdDetails
{
    public string? PayId { get; set; }

    public string? PayIdType { get; set; }
}
