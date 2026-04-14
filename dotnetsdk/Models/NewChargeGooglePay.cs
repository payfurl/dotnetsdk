using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewChargeGooglePay : NewCustomerEmailAndPhoneData
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public GooglePaymentData PaymentData { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Descriptor { get; set; }
        public string UserAgent { get; set; }
        public Order Order { get; set; }
    }

    public class GooglePaymentData
    {
        public GooglePaymentMethodData PaymentMethodData { get; set; }
        public GooglePaymentInfo PaymentInfo { get; set; }
    }

    public class GooglePaymentInfo
    {
        public string CardDetails { get; set; }
        public string CardNetwork { get; set; }
    }

    public class GooglePaymentMethodData
    {
        public GooglePaymentMethodDataTokenizationData TokenizationData { get; set; }
    }

    public class GooglePaymentMethodDataTokenizationData
    {
        public IntermediateSigningKey IntermediateSigningKey { get; set; }
        public string ProtocolVersion { get; set; }
        public string Signature { get; set; }
        public string SignedMessage { get; set; }
    }

    public class IntermediateSigningKey
    {
        public List<string> Signatures { get; set; }
        public string SignedKey { get; set; }
    }
}
