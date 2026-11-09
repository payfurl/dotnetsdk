using System.Runtime.Serialization;

namespace Payfurl.Sdk.Models;

/// <summary>
/// What a webhook subscription receives. The API's <c>WebhookType</c> (_shared/src/common/Models/WebhookType.cs).
///
/// <c>Unknown</c> is a value this version of the SDK does not know yet.
/// </summary>
public enum WebhookType
{
    /// <summary>A value this version of the SDK does not know yet. Never sent.</summary>
    Unknown = 0,

    /// <summary><c>Transaction</c></summary>
    [EnumMember(Value = "Transaction")]
    Transaction,

    /// <summary><c>Batch</c></summary>
    [EnumMember(Value = "Batch")]
    Batch,

    /// <summary><c>Subscription</c></summary>
    [EnumMember(Value = "Subscription")]
    Subscription,

    /// <summary><c>Customer</c></summary>
    [EnumMember(Value = "Customer")]
    Customer,

    /// <summary><c>PaymentMethod</c></summary>
    [EnumMember(Value = "PaymentMethod")]
    PaymentMethod,

    /// <summary><c>Dispute</c></summary>
    [EnumMember(Value = "Dispute")]
    Dispute,
}
