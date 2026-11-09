using System.Runtime.Serialization;

namespace Payfurl.Sdk.Models;

/// <summary>
/// A charge's status (<c>ChargeData::status</c>). The API's <c>TransactionStatus</c> (_shared/src/api.core/Data/Transaction.cs).
///
/// <c>Unknown</c> is a value this version of the SDK does not know yet.
/// </summary>
public enum TransactionStatus
{
    /// <summary>A value this version of the SDK does not know yet. Never sent.</summary>
    Unknown = 0,

    /// <summary><c>PENDING</c></summary>
    [EnumMember(Value = "PENDING")]
    Pending,

    /// <summary><c>FAILED</c></summary>
    [EnumMember(Value = "FAILED")]
    Failed,

    /// <summary><c>SUCCESS</c></summary>
    [EnumMember(Value = "SUCCESS")]
    Success,

    /// <summary><c>PARTIAL_REFUND</c></summary>
    [EnumMember(Value = "PARTIAL_REFUND")]
    PartialRefund,

    /// <summary><c>REFUND</c></summary>
    [EnumMember(Value = "REFUND")]
    Refund,

    /// <summary><c>PENDING_CHECKOUT</c></summary>
    [EnumMember(Value = "PENDING_CHECKOUT")]
    PendingCheckout,

    /// <summary><c>AUTHORISE</c></summary>
    [EnumMember(Value = "AUTHORISE")]
    Authorise,

    /// <summary><c>AUTHORISE_CANCELLED</c></summary>
    [EnumMember(Value = "AUTHORISE_CANCELLED")]
    AuthoriseCancelled,
}
