#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>VaultData</c> (api/src/api/Models/VaultData.cs), in a response.
///
/// Every property is null when the response leaves it out.
/// </summary>
public sealed class VaultData
{
    public string? VaultId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public string? MaskedCardNumber { get; set; }

    public bool? HasCvv { get; set; }
}
