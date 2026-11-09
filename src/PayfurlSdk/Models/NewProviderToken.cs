#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewProviderToken</c> (api/src/api/Models/NewProviderToken.cs), in a request.
/// </summary>
public sealed class NewProviderToken
{
    public required string ProviderId { get; set; }

    public required string ProviderToken { get; set; }

    public string? Email { get; set; }

    public Dictionary<string, string>? ProviderTokenData { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }

    public bool? Verify { get; set; }

    public PaymentType? Type { get; set; }

    public string? FallbackPaymentMethodId { get; set; }
}
