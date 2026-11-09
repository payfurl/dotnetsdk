#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>PaymentMethodData</c> (api/src/api/Models/PaymentMethodData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class PaymentMethodData
{
    public string? PaymentMethodId { get; set; }

    public string? CustomerId { get; set; }

    public PaymentType? Type { get; set; }

    public CardData? Card { get; set; }

    public BankPaymentData? BankPayment { get; set; }

    public string? ProviderId { get; set; }

    public string? ProviderType { get; set; }

    public DateTime? DateAdded { get; set; }

    public string? Email { get; set; }

    public CustomerDataSummary? Customer { get; set; }

    public ProviderSummary? Provider { get; set; }

    public PayToSummary? PayTo { get; set; }

    public NetworkTokenSummary? NetworkToken { get; set; }

    public string? VaultId { get; set; }

    public string? PayToStatus { get; set; }

    public string? Ip { get; set; }

    public DateTime? DateRemoved { get; set; }

    public string? ProviderPaymentMethodId { get; set; }

    public Dictionary<string, string>? ProviderPaymentMethodData { get; set; }

    public string? Token { get; set; }

    public string? NetworkTokenId { get; set; }

    public string? FallbackPaymentMethodId { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }
}
