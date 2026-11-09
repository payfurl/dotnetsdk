#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>CustomerDataSummary</c> (_shared/src/common/Models/CustomerSummary.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class CustomerDataSummary
{
    public string? CustomerId { get; set; }

    public string? Reference { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateRemoved { get; set; }
}
