#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>UpdatePaymentMethod</c> (api/src/api/Models/UpdatePaymentMethod.cs), in a request.
/// </summary>
public sealed class UpdatePaymentMethod
{
    public required UpdatePaymentMethodCardRequestInformation Card { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }
}
