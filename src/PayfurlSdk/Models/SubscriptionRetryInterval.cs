using System.Runtime.Serialization;

namespace Payfurl.Sdk.Models;

/// <summary>
/// The unit of a subscription's retry frequency. The API's <c>SubscriptionRetryInterval</c> (_shared/src/api.core/Data/Subscriptions/SubscriptionRetryInterval.cs).
///
/// <c>Unknown</c> is a value this version of the SDK does not know yet.
/// </summary>
public enum SubscriptionRetryInterval
{
    /// <summary>A value this version of the SDK does not know yet. Never sent.</summary>
    Unknown = 0,

    /// <summary><c>Hour</c></summary>
    [EnumMember(Value = "Hour")]
    Hour,

    /// <summary><c>Day</c></summary>
    [EnumMember(Value = "Day")]
    Day,
}
