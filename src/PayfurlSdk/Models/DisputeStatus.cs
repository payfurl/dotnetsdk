using System.Runtime.Serialization;

namespace Payfurl.Sdk.Models;

/// <summary>
/// A dispute's status (<see cref="Dispute.Status"/>): the API's <c>DisputeStatus</c> (_shared/src/common/Models/DisputeEnums.cs).
///
/// <c>Unknown</c> is the API's own <c>UNKNOWN</c>, or a value this version of the SDK does not know yet.
/// </summary>
public enum DisputeStatus
{
    /// <summary><c>UNKNOWN</c>, or a value this version of the SDK does not know yet.</summary>
    [EnumMember(Value = "UNKNOWN")]
    Unknown = 0,

    /// <summary><c>WARNING_NEEDS_RESPONSE</c></summary>
    [EnumMember(Value = "WARNING_NEEDS_RESPONSE")]
    WarningNeedsResponse,

    /// <summary><c>WARNING_UNDER_REVIEW</c></summary>
    [EnumMember(Value = "WARNING_UNDER_REVIEW")]
    WarningUnderReview,

    /// <summary><c>WARNING_CLOSED</c></summary>
    [EnumMember(Value = "WARNING_CLOSED")]
    WarningClosed,

    /// <summary><c>NEEDS_RESPONSE</c></summary>
    [EnumMember(Value = "NEEDS_RESPONSE")]
    NeedsResponse,

    /// <summary><c>UNDER_REVIEW</c></summary>
    [EnumMember(Value = "UNDER_REVIEW")]
    UnderReview,

    /// <summary><c>WON</c></summary>
    [EnumMember(Value = "WON")]
    Won,

    /// <summary><c>LOST</c></summary>
    [EnumMember(Value = "LOST")]
    Lost,

    /// <summary><c>PREVENTED</c></summary>
    [EnumMember(Value = "PREVENTED")]
    Prevented,
}
