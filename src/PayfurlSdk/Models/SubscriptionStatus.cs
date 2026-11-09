using System.Runtime.Serialization;

namespace Payfurl.Sdk.Models;

/// <summary>
/// A subscription's status. The API's <c>SubscriptionStatus</c> (_shared/src/api.core/Data/Subscriptions/SubscriptionStatus.cs).
///
/// <c>Unknown</c> is a value this version of the SDK does not know yet.
/// </summary>
public enum SubscriptionStatus
{
    /// <summary>A value this version of the SDK does not know yet, or the API's own <c>Unknown</c>.</summary>
    [EnumMember(Value = "Unknown")]
    Unknown = 0,

    /// <summary><c>Complete</c></summary>
    [EnumMember(Value = "Complete")]
    Complete,

    /// <summary><c>Active</c></summary>
    [EnumMember(Value = "Active")]
    Active,

    /// <summary><c>Cancelled</c></summary>
    [EnumMember(Value = "Cancelled")]
    Cancelled,

    /// <summary><c>Suspended</c></summary>
    [EnumMember(Value = "Suspended")]
    Suspended,
}
