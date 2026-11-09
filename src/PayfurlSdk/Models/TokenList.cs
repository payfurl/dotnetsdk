#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>TokenDataList</c> (api/src/api/Models/TokenDataList.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class TokenList
{
    public int? Limit { get; set; }

    public int? Skip { get; set; }

    public int? Count { get; set; }

    public List<TokenData>? Tokens { get; set; }
}
