using System.Runtime.Serialization;

namespace Payfurl.Sdk.Models;

/// <summary>
/// Who started a charge: the customer at checkout, or the merchant (a stored card, a subscription). The API's <c>Initiator</c> (api/src/api/Models/Initiator.cs).
///
/// <c>Unknown</c> is a value this version of the SDK does not know yet.
/// </summary>
public enum Initiator
{
    /// <summary>A value this version of the SDK does not know yet. Never sent.</summary>
    Unknown = 0,

    /// <summary><c>CUSTOMER</c></summary>
    [EnumMember(Value = "CUSTOMER")]
    Customer,

    /// <summary><c>MERCHANT</c></summary>
    [EnumMember(Value = "MERCHANT")]
    Merchant,
}
