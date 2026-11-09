#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>GooglePayProviderInfo</c> (_shared/src/api.core/DTO/GooglePayProviderInfo.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class GooglePayProviderInfo
{
    public string? Gateway { get; set; }

    public string? GooglePayGatewayMerchantId { get; set; }

    public string? GooglePayMerchantId { get; set; }

    public string? GooglePayMerchantName { get; set; }

    public string? ProviderId { get; set; }
}
