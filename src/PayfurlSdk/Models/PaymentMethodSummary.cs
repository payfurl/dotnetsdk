#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>PaymentMethodSummary</c> (api/src/api/Models/PaymentMethodSummary.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class PaymentMethodSummary
{
    public string? PaymentMethodId { get; set; }

    public string? CustomerId { get; set; }

    public PaymentType? Type { get; set; }

    public CardData? Card { get; set; }

    public string? ProviderId { get; set; }

    public string? ProviderType { get; set; }

    public DateTime? DateAdded { get; set; }

    public string? Email { get; set; }

    public string? VaultId { get; set; }

    public DateTime? DateRemoved { get; set; }

    public string? ProviderPaymentMethodId { get; set; }

    public Dictionary<string, string>? ProviderPaymentMethodData { get; set; }

    public string? Token { get; set; }

    public string? FallbackPaymentMethodId { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }

    public string? NetworkTokenId { get; set; }
}
