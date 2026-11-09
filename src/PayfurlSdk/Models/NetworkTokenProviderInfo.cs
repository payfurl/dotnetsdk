#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NetworkTokenProviderInfo</c> (_shared/src/api.core/DTO/AvailablePaymentTypes.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class NetworkTokenProviderInfo
{
    public string? ProviderId { get; set; }

    public string? ProviderType { get; set; }

    public Dictionary<string, string>? ClientParameters { get; set; }
}
