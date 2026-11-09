#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>PayToSummary</c> (_shared/src/common/Models/PayToSummary.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class PayToSummary
{
    public string? PayToId { get; set; }

    public string? Status { get; set; }

    public string? PayerName { get; set; }

    public PayerPayIdDetails? PayerPayIdDetails { get; set; }

    public DirectDebitDetails? PayerDirectDebitDetails { get; set; }

    public string? ProviderId { get; set; }

    public string? ProviderType { get; set; }
}
