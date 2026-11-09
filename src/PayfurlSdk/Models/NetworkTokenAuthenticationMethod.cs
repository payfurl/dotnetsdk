#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NetworkTokenAuthenticationMethod</c> (_shared/src/api.core/Data/NetworkTokenAuthenticationMethod.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class NetworkTokenAuthenticationMethod
{
    public string? AuthenticationMethodType { get; set; }

    public string? AuthenticationSubject { get; set; }

    public UriData? UriData { get; set; }
}
