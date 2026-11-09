#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Payfurl.Sdk.Internal;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>PaymentLinkModel</c> (api/src/api/Models/PaymentLink/PaymentLinkModel.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class PaymentLink
{
    public string? Url { get; set; }

    public string? PaymentLinkId { get; set; }

    public DateTime? DateAdded { get; set; }

    public string? AccountId { get; set; }

    public string? PublicKey { get; set; }

    public string? Title { get; set; }

    public decimal? Amount { get; set; }

    public string? Currency { get; set; }

    /// <summary>
    /// The payment methods the link's checkout offers, as the browser SDK names them: <c>card</c>, <c>paypal</c>,
    /// <c>googlepay</c>, <c>applepay</c>, <c>azupay</c>, ... The payment link passes them to its drop-in, which reads
    /// them case-insensitively.
    /// </summary>
    [JsonPropertyName("allowedPaymentTypes")]
    public List<string>? AllowedPaymentTypeNames { get; set; }

    /// <summary>
    /// <see cref="AllowedPaymentTypeNames"/> as <see cref="PaymentType"/>: a name no case matches (<c>azupay</c>)
    /// reads as <see cref="PaymentType.Unknown"/>, and setting it replaces the names. A copy: adding to it changes
    /// nothing.
    /// </summary>
    [JsonIgnore]
    [Obsolete("The API takes the browser SDK's names, which PaymentType doesn't cover: use AllowedPaymentTypeNames.")]
    public List<PaymentType>? AllowedPaymentTypes
    {
        get => PaymentTypeNames.ToTypes(AllowedPaymentTypeNames);
        set => AllowedPaymentTypeNames = PaymentTypeNames.ToNames(value);
    }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? ConfirmationMessage { get; set; }

    public string? RedirectUrl { get; set; }

    public CallToAction? CallToAction { get; set; }

    public int? LimitPayments { get; set; }

    public int? CountPayments { get; set; }

    public string? InvoiceId { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }
}
