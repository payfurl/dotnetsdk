#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>DisputeResponse</c> (_shared/src/common/Models/DisputeResponse.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class Dispute
{
    public string? DisputeId { get; set; }

    public string? TransactionId { get; set; }

    public string? ProviderId { get; set; }

    public string? Type { get; set; }

    public string? GatewayDisputeId { get; set; }

    public decimal? Amount { get; set; }

    public string? Currency { get; set; }

    public decimal? Fee { get; set; }

    public DisputeStatus? Status { get; set; }

    public DisputeStage? Stage { get; set; }

    public string? ReasonCategory { get; set; }

    public string? Reason { get; set; }

    public string? NetworkReasonCode { get; set; }

    public string? CardBrand { get; set; }

    public DateTime? DisputeDate { get; set; }

    public DateTime? EvidenceDueBy { get; set; }

    public int? SubmissionCount { get; set; }

    public bool? IsChargeRefundable { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }
}
