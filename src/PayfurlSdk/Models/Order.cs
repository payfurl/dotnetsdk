#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>Order</c> (api/src/api/Models/Address.cs), in a request.
/// </summary>
public sealed class Order
{
    public string? OrderNumber { get; set; }

    public decimal? FreightAmount { get; set; }

    public decimal? DutyAmount { get; set; }

    public List<ProductItem>? Items { get; set; }
}
