#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewTransferGroup</c> (api/src/api/Models/NewTransferGroup.cs), in a request.
/// </summary>
public sealed class NewTransferGroup
{
    public required string ProviderId { get; set; }

    /// <summary>A reference for the group, unique on the account. Optional: the API checks it for duplicates only when it is set.</summary>
    public string? GroupReference { get; set; }

    public required List<NewTransfer> Transfers { get; set; }

    public string? ChargeId { get; set; }
}
