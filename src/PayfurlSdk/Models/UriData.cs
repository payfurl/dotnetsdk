#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>UriData</c> (_shared/src/api.core/Data/UriData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class UriData
{
    public string? Uri { get; set; }

    public string? UriType { get; set; }
}
