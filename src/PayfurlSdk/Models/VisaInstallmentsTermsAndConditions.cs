#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>VisaInstallmentsTermsAndConditions</c> (_shared/src/api.core/DTO/VisaInstallmentsPlan.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class VisaInstallmentsTermsAndConditions
{
    public string? Url { get; set; }

    public int? Version { get; set; }

    public string? Text { get; set; }

    public string? LanguageCode { get; set; }
}
