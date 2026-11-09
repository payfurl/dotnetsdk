#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewCustomerToken</c> (api/src/api/Models/NewCustomerToken.cs), in a request.
/// </summary>
public sealed class NewCustomerToken
{
    public required string Token { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Reference { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Ip { get; set; }

    public Address? Address { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }

    public WebhookConfig? Webhook { get; set; }
}
