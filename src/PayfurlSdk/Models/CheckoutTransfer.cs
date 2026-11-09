#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>CheckoutTransfer</c> (api/src/api/Models/CheckoutTransfer.cs), in a request.
/// </summary>
public sealed class CheckoutTransfer
{
    public required decimal Amount { get; set; }

    public string? Account { get; set; }
}
