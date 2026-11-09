#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ProviderSummary</c> (_shared/src/common/Models/ProviderSummary.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class ProviderSummary
{
    public string? ProviderId { get; set; }

    public string? Type { get; set; }

    public string? Name { get; set; }
}
