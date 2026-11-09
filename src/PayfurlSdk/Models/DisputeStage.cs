using System.Runtime.Serialization;

namespace Payfurl.Sdk.Models;

/// <summary>
/// A dispute's stage (<see cref="Dispute.Stage"/>): the API's <c>DisputeStage</c> (_shared/src/common/Models/DisputeEnums.cs).
///
/// <c>Unknown</c> is the API's own <c>UNKNOWN</c>, or a value this version of the SDK does not know yet.
/// </summary>
public enum DisputeStage
{
    /// <summary><c>UNKNOWN</c>, or a value this version of the SDK does not know yet.</summary>
    [EnumMember(Value = "UNKNOWN")]
    Unknown = 0,

    /// <summary><c>INQUIRY</c></summary>
    [EnumMember(Value = "INQUIRY")]
    Inquiry,

    /// <summary><c>CHARGEBACK</c></summary>
    [EnumMember(Value = "CHARGEBACK")]
    Chargeback,

    /// <summary><c>COMPLIANCE</c></summary>
    [EnumMember(Value = "COMPLIANCE")]
    Compliance,

    /// <summary><c>BLOCK</c></summary>
    [EnumMember(Value = "BLOCK")]
    Block,

    /// <summary><c>RESOLUTION</c></summary>
    [EnumMember(Value = "RESOLUTION")]
    Resolution,
}
