#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>UpdateCustomer</c> (api/src/api/Models/UpdateCustomer.cs), in a request.
/// </summary>
public sealed class UpdateCustomer
{
    public string? Email { get; set; }

    public string? Phone { get; set; }

    public Address? Address { get; set; }

    public string? DefaultPaymentMethodId { get; set; }
}
