#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ProcessorResult</c> (_shared/src/common/Models/ProcessorResult.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class ProcessorResult
{
    public string? AuthorizationCode { get; set; }

    public string? ResponseCode { get; set; }

    public string? ResponseText { get; set; }

    public string? CvvResult { get; set; }

    public string? AvsStreetResult { get; set; }

    public string? AvsZipResult { get; set; }

    public string? AvsNameResult { get; set; }

    public string? AvsFullResult { get; set; }

    public string? NetworkTransactionId { get; set; }
}
