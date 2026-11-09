using System.Runtime.Serialization;

namespace Payfurl.Sdk.Models;

/// <summary>
/// A payment link's button. The API's <c>CallToAction</c> (_shared/src/common/Models/CallToAction.cs).
///
/// <c>Unknown</c> is a value this version of the SDK does not know yet.
/// </summary>
public enum CallToAction
{
    /// <summary>A value this version of the SDK does not know yet. Never sent.</summary>
    Unknown = 0,

    /// <summary><c>Pay</c></summary>
    [EnumMember(Value = "Pay")]
    Pay,

    /// <summary><c>Donate</c></summary>
    [EnumMember(Value = "Donate")]
    Donate,
}
