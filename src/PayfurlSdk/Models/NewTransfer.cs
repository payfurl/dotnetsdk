#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewTransfer</c> (api/src/api/Models/TransferItem.cs), in a request.
/// </summary>
public sealed class NewTransfer
{
    public required string Account { get; set; }

    public required decimal Amount { get; set; }

    public string? Currency { get; set; }

    public string? Message { get; set; }

    public string? Reference { get; set; }
}
