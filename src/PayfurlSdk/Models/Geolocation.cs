#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>Geolocation</c> (api/src/api/Models/Geolocation.cs), in a request.
/// </summary>
public sealed class Geolocation
{
    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }
}
