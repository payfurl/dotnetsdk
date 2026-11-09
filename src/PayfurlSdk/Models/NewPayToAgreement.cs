#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewPayToAgreement</c> (api/src/api/Models/NewPayToAgreement.cs), in a request.
/// </summary>
public sealed class NewPayToAgreement
{
    public required string PayerName { get; set; }

    public required PayIdDetails PayerPayIdDetails { get; set; }

    public required string Description { get; set; }

    public required int MaximumAmount { get; set; }

    public required string ProviderId { get; set; }

    public string? NotificationUrl { get; set; }

    public string? Ip { get; set; }

    public bool? SetDefault { get; set; }

    public string? FallbackPaymentMethodId { get; set; }

    public WebhookConfig? Webhook { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }
}
