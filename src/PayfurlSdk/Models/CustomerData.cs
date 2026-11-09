#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>CustomerData</c> (api/src/api/Models/CustomerData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class CustomerData
{
    public string? CustomerId { get; set; }

    public string? Reference { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? DateAdded { get; set; }

    public PaymentMethodSummary? DefaultPaymentMethod { get; set; }

    public string? Ip { get; set; }

    public DateTime? DateRemoved { get; set; }

    public Address? Address { get; set; }
}
