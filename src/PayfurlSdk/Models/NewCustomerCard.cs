#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewCustomerCard</c> (api/src/api/Models/NewCustomerCard.cs), in a request.
/// </summary>
public sealed class NewCustomerCard
{
    public required string ProviderId { get; set; }

    public required CardRequestInformation PaymentInformation { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Reference { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Ip { get; set; }

    public Address? Address { get; set; }

    public bool? VaultCard { get; set; }

    public DateTime? VaultExpireDate { get; set; }

    public int? VaultExpireSeconds { get; set; }

    public bool? SkipExpiryDateValidation { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }

    public WebhookConfig? Webhook { get; set; }

    public bool? CreateNetworkToken { get; set; }

    public bool? Verify { get; set; }
}
