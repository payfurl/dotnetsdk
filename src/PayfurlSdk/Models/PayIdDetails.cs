#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>PayIdDetails</c> (api/src/api/Models/NewPayToAgreement.cs), in a request.
/// </summary>
public sealed class PayIdDetails
{
    public required string PayId { get; set; }

    public required string PayIdType { get; set; }
}
