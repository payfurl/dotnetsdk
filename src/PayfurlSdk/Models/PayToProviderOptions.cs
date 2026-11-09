#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>PayToProviderOptions</c> (_shared/src/api.core/DTO/AvailablePaymentTypes.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class PayToProviderOptions
{
    public string? ProviderId { get; set; }

    public decimal? MaximumAmount { get; set; }

    public string? Description { get; set; }
}
