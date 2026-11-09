#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewPaymentMethodToken</c> (api/src/api/Models/NewPaymentMethodToken.cs), in a request.
/// </summary>
public sealed class NewPaymentMethodToken
{
    public required string Token { get; set; }

    public string? Ip { get; set; }

    public bool? SetDefault { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }

    public string? FallbackPaymentMethodId { get; set; }

    public WebhookConfig? Webhook { get; set; }
}
