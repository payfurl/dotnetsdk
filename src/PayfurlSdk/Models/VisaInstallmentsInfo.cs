#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>VisaInstallmentsInfo</c> (_shared/src/api.core/DTO/NewPaymentToken.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class VisaInstallmentsInfo
{
    public string? ProviderId { get; set; }

    public string? VisPlanId { get; set; }

    public int? VisAcceptedTAndCVersion { get; set; }

    public string? VisPlanIdRef { get; set; }

    public string? VisPlanName { get; set; }

    public int? VisNumberOfInstallments { get; set; }

    public decimal? VisTotalFees { get; set; }

    public decimal? VisTotalPlanCost { get; set; }

    public string? VisInstallmentFrequency { get; set; }

    public List<VisaInstallmentsTermsAndConditions>? VisTermsAndConditions { get; set; }
}
