#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>ClickToPayOptions</c> (_shared/src/api.core/DTO/AvailablePaymentTypes.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class ClickToPayOptions
{
    public string? DpaId { get; set; }

    public string? DpaName { get; set; }

    public bool? UseFpan { get; set; }
}
