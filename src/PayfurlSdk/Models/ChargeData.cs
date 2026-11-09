#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ChargeData</c> (_shared/src/common/Models/ChargeData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class ChargeData
{
    public string? ChargeId { get; set; }

    public string? ProviderChargeId { get; set; }

    public decimal? Amount { get; set; }

    public string? ProviderId { get; set; }

    public string? Reference { get; set; }

    public PaymentData? PaymentInformation { get; set; }

    public string? CustomerId { get; set; }

    public TransactionStatus? Status { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? SuccessDate { get; set; }

    public DateTime? VoidDate { get; set; }

    public decimal? RefundedAmount { get; set; }

    public decimal? EstimatedCost { get; set; }

    public string? EstimatedCostCurrency { get; set; }

    public string? Currency { get; set; }

    public List<Refund>? Refunds { get; set; }

    public List<FailureData>? FailedAttempts { get; set; }

    public CustomerDataSummary? Customer { get; set; }

    public ProviderSummary? Provider { get; set; }

    public bool? ThreeDsVerified { get; set; }

    public decimal? AuthorisationAmount { get; set; }

    public string? Initiator { get; set; }

    public string? Ip { get; set; }

    public GeolocationInfo? IpInfo { get; set; }

    public VisaInstallmentsInfo? VisaInstallments { get; set; }

    public string? Descriptor { get; set; }

    public string? ThreeDsRedirectUrl { get; set; }

    public string? PaymentTokenId { get; set; }

    public string? SubscriptionId { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }
}
