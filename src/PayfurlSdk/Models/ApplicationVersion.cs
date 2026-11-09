#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ApplicationVersion</c> (api/src/api/Models/Version.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class ApplicationVersion
{
    public string? Version { get; set; }

    public DateTime? Released { get; set; }

    public string? Region { get; set; }
}
