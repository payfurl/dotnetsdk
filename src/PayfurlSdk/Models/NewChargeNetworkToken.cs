#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewChargeNetworkToken</c> (api/src/api/Models/NewChargeNetworkToken.cs), in a request.
/// </summary>
public sealed class NewChargeNetworkToken
{
    public required string NetworkTokenId { get; set; }

    public required decimal Amount { get; set; }

    public required string ProviderId { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Currency { get; set; }

    public string? Reference { get; set; }

    public bool? Capture { get; set; }

    public string? Ip { get; set; }

    public Address? Address { get; set; }

    public Order? Order { get; set; }

    public string? CustomerCode { get; set; }

    public string? InvoiceNumber { get; set; }

    public Initiator? Initiator { get; set; }

    public WebhookConfig? Webhook { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }

    public string? Descriptor { get; set; }

    public string? ThreeDSNotificationUrl { get; set; }
}
