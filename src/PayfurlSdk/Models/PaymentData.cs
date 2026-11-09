#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>PaymentData</c> (_shared/src/common/Models/PaymentData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class PaymentData
{
    public string? PaymentMethodId { get; set; }

    public CardData? Card { get; set; }

    public PaymentType? Type { get; set; }

    public string? DisplayType { get; set; }

    public string? Email { get; set; }

    public BankPaymentData? BankPayment { get; set; }
}
