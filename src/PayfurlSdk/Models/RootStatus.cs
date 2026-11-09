#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>Status</c> (api/src/api/Models/Status.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class RootStatus
{
    public string? Status { get; set; }
}
