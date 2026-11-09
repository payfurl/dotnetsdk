#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>CreatePaymentLink</c> (api/src/api/Models/PaymentLink/CreatePaymentLink.cs), in a request.
/// </summary>
public sealed class CreatePaymentLink
{
    public required string Title { get; set; }

    public decimal? Amount { get; set; }

    public string? Currency { get; set; }

    public List<PaymentType>? AllowedPaymentTypes { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? ConfirmationMessage { get; set; }

    public string? RedirectUrl { get; set; }

    public CallToAction? CallToAction { get; set; }

    public int? LimitPayments { get; set; }

    public string? InvoiceId { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }
}
