#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>FailureData</c> (_shared/src/api.core/Data/FailureData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class FailureData
{
    public string? ProviderId { get; set; }

    public DateTime? DateAttempted { get; set; }

    public string? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public string? GatewayCode { get; set; }

    public string? GatewayMessage { get; set; }

    public ProcessorResult? ProcessorResult { get; set; }

    public bool? IsRetryable { get; set; }
}
