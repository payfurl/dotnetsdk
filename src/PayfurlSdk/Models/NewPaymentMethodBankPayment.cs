#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewPaymentMethodBankPayment</c> (api/src/api/Models/NewPaymentMethodBankPayment.cs), in a request.
/// </summary>
public sealed class NewPaymentMethodBankPayment
{
    public required string ProviderId { get; set; }

    public required NewBankPayment BankPaymentInformation { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }

    public bool? SetDefault { get; set; }

    public string? FallbackPaymentMethodId { get; set; }

    public WebhookConfig? Webhook { get; set; }
}
