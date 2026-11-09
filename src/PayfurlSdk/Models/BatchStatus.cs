#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>BatchStatus</c> (_shared/src/common/Models/BatchStatus.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class BatchStatus
{
    public string? BatchId { get; set; }

    public int? Count { get; set; }

    public int? Success { get; set; }

    public int? Failure { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public decimal? Progress { get; set; }

    public DateTime? DateAdded { get; set; }

    public Dictionary<string, string>? Metadata { get; set; }
}
