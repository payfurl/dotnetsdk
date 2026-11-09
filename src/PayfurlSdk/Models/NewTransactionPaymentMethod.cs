#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewTransactionPaymentMethod</c> (api/src/api/Models/Batch/NewTransactionPaymentMethod.cs), in a request.
/// </summary>
public sealed class NewTransactionPaymentMethod
{
    public required int Count { get; set; }

    public required string Description { get; set; }

    public required string Batch { get; set; }

    public WebhookConfig? Webhook { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }
}
