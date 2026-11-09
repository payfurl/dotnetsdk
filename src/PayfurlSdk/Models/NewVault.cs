#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System;
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewVault</c> (api/src/api/Models/NewVault.cs), in a request.
/// </summary>
public sealed class NewVault
{
    public required string CardNumber { get; set; }

    public string? Ccv { get; set; }

    public DateTime? ExpireDate { get; set; }

    public int? ExpireSeconds { get; set; }
}
