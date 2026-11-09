#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewProvider</c> (api/src/api/Models/NewProvider.cs), in a request.
/// </summary>
public sealed class NewProvider
{
    public required string Type { get; set; }

    public required string Name { get; set; }

    public required string Environment { get; set; }

    public required Dictionary<string, string> AuthenticationParameters { get; set; }

    public required string Currency { get; set; }

    public Dictionary<string, string>? AdditionalParameters { get; set; }

    public string? ProviderCountry { get; set; }
}
