#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>BankPaymentData</c> (_shared/src/common/Models/BankPaymentData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class BankPaymentData
{
    public string? BankCode { get; set; }

    public string? AccountNumber { get; set; }

    public string? AccountName { get; set; }
}
