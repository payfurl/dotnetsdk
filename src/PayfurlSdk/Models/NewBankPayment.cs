#pragma warning disable CS1591 // The properties are the API's fields, with the API's names.
using System.Collections.Generic;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The API's <c>NewBankPayment</c> (api/src/api/Models/NewBankPayment.cs), in a request.
/// </summary>
public sealed class NewBankPayment
{
    public required string BankCode { get; set; }

    public required string AccountNumber { get; set; }

    public required string AccountName { get; set; }
}
