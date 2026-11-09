#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>UpdateProvider</c> (api/src/api/Models/UpdateProvider.cs), in a request.
/// </summary>
public sealed class UpdateProvider
{
    public string? Name { get; set; }

    public Dictionary<string, string>? AuthenticationParameters { get; set; }

    public Dictionary<string, string>? AdditionalParameters { get; set; }

    public string? ProviderCountry { get; set; }

    public string? Currency { get; set; }
}
