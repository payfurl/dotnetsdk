#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>GeolocationInfo</c> (_shared/src/common/Geolocation/IGeolocationDownloader.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class GeolocationInfo
{
    public string? Ip { get; set; }

    public string? Hostname { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? Country { get; set; }

    public string? Loc { get; set; }

    public string? Org { get; set; }

    public string? Postal { get; set; }

    public string? Timezone { get; set; }
}
