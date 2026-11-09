using System.Runtime.Serialization;

namespace Payfurl.Sdk.Models;

/// <summary>
/// A payment method's type. The API's <c>PaymentType</c> (_shared/src/api.core/Data/PaymentMethod.cs).
///
/// <c>Unknown</c> is a value this version of the SDK does not know yet.
/// </summary>
public enum PaymentType
{
    /// <summary>A value this version of the SDK does not know yet. Never sent.</summary>
    Unknown = 0,

    /// <summary><c>CARD</c></summary>
    [EnumMember(Value = "CARD")]
    Card,

    /// <summary><c>PAYPAL</c></summary>
    [EnumMember(Value = "PAYPAL")]
    PayPal,

    /// <summary><c>PAYTO</c></summary>
    [EnumMember(Value = "PAYTO")]
    PayTo,

    /// <summary><c>PAYID</c></summary>
    [EnumMember(Value = "PAYID")]
    PayId,

    /// <summary><c>OPTTY</c></summary>
    [EnumMember(Value = "OPTTY")]
    Optty,

    /// <summary><c>GOOGLEPAY</c></summary>
    [EnumMember(Value = "GOOGLEPAY")]
    GooglePay,

    /// <summary><c>APPLEPAY</c></summary>
    [EnumMember(Value = "APPLEPAY")]
    ApplePay,

    /// <summary><c>VIS</c></summary>
    [EnumMember(Value = "VIS")]
    Vis,

    /// <summary><c>SHIFT</c></summary>
    [EnumMember(Value = "SHIFT")]
    Shift,

    /// <summary><c>PESAPAL</c></summary>
    [EnumMember(Value = "PESAPAL")]
    Pesapal,

    /// <summary><c>PAYBYACCOUNT</c></summary>
    [EnumMember(Value = "PAYBYACCOUNT")]
    PayByAccount,

    /// <summary><c>CLICK2PAY</c></summary>
    [EnumMember(Value = "CLICK2PAY")]
    ClickToPay,

    /// <summary><c>UPI</c></summary>
    [EnumMember(Value = "UPI")]
    Upi,

    /// <summary><c>PAYGLOCAL</c></summary>
    [EnumMember(Value = "PAYGLOCAL")]
    PayGlocal,

    /// <summary><c>BANKPAYMENT</c></summary>
    [EnumMember(Value = "BANKPAYMENT")]
    BankPayment,

    /// <summary><c>PAYMENT_METHOD</c></summary>
    [EnumMember(Value = "PAYMENT_METHOD")]
    PaymentMethod,

    /// <summary><c>ZIP</c></summary>
    [EnumMember(Value = "ZIP")]
    Zip,
}
