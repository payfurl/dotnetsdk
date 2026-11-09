#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>FraudOptions</c> (_shared/src/api.core/DTO/AvailablePaymentTypes.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class FraudOptions
{
    public string? ProviderId { get; set; }

    public string? ProviderType { get; set; }

    public bool? Enabled { get; set; }

    public bool? StopPaymentOnFailure { get; set; }

    public Dictionary<string, string>? ClientParameters { get; set; }
}
