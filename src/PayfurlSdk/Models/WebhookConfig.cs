#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>WebhookConfig</c> (api/src/api/Models/WebhookConfig.cs), in a request and response.
/// </summary>
public sealed class WebhookConfig
{
    public string? Url { get; set; }

    public string? Authorization { get; set; }
}
