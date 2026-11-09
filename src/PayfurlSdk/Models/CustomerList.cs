#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>CustomerList</c> (api/src/api/Models/CustomerList.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class CustomerList
{
    public int? Limit { get; set; }

    public int? Skip { get; set; }

    public int? Count { get; set; }

    public List<CustomerData>? Customers { get; set; }
}
