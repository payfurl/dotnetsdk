#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ProductItem</c> (api/src/api/Models/Address.cs), in a request.
/// </summary>
public sealed class ProductItem
{
    public string? ProductCode { get; set; }

    public string? CommodityCode { get; set; }

    public string? Description { get; set; }

    public int? Quantity { get; set; }

    public string? UnitOfMeasure { get; set; }

    public decimal? Amount { get; set; }

    public decimal? TaxAmount { get; set; }
}
