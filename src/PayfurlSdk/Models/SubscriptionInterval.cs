using System.Runtime.Serialization;

namespace Payfurl.Sdk.Models;

/// <summary>
/// How often a subscription charges, with its frequency. The API's <c>SubscriptionInterval</c> (_shared/src/api.core/Data/Subscriptions/SubscriptionInterval.cs).
///
/// <c>Unknown</c> is a value this version of the SDK does not know yet.
/// </summary>
public enum SubscriptionInterval
{
    /// <summary>A value this version of the SDK does not know yet. Never sent.</summary>
    Unknown = 0,

    /// <summary><c>Day</c></summary>
    [EnumMember(Value = "Day")]
    Day,

    /// <summary><c>Week</c></summary>
    [EnumMember(Value = "Week")]
    Week,

    /// <summary><c>Month</c></summary>
    [EnumMember(Value = "Month")]
    Month,

    /// <summary><c>Year</c></summary>
    [EnumMember(Value = "Year")]
    Year,
}
