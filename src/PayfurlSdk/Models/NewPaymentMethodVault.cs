#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewPaymentMethodVault</c> (api/src/api/Models/NewPaymentMethodVault.cs), in a request.
/// </summary>
public sealed class NewPaymentMethodVault
{
    public required string ProviderId { get; set; }

    public required string PaymentMethodId { get; set; }

    public string? Ccv { get; set; }

    public string? FallbackPaymentMethodId { get; set; }

    public WebhookConfig? Webhook { get; set; }
}
