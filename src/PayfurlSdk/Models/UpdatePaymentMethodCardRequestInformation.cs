#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>UpdatePaymentMethodCardRequestInformation</c> (api/src/api/Models/UpdatePaymentMethod.cs), in a request.
/// </summary>
public sealed class UpdatePaymentMethodCardRequestInformation
{
    public string? ExpiryDate { get; set; }

    public string? Cardholder { get; set; }

    public string? CardNumber { get; set; }
}
