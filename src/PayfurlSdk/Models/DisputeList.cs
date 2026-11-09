#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>DisputeList</c> (api/src/api/Models/DisputeList.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class DisputeList
{
    public int? Limit { get; set; }

    public int? Skip { get; set; }

    public int? Count { get; set; }

    public List<Dispute>? Disputes { get; set; }
}
