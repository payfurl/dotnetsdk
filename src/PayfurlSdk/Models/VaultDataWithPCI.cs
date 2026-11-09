#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>VaultDataWithPCI</c> (api/src/api/Models/VaultDataWithPCI.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class VaultDataWithPCI
{
    public string? VaultId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public string? MaskedCardNumber { get; set; }

    public bool? HasCvv { get; set; }

    public string? CardNumber { get; set; }

    public string? Ccv { get; set; }
}
