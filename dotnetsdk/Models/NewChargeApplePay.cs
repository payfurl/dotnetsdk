namespace payfurl.sdk.Models
{
    public class NewChargeApplePay : NewCustomerEmailAndPhoneData
    {
        public AppleToken Token { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Descriptor { get; set; }
        public string UserAgent { get; set; }
        public Order Order { get; set; }
    }

    public class AppleToken
    {
        public ApplePaySource PaymentData { get; set; }
        public ApplePaymentMethod PaymentMethod { get; set; }
    }

    public class ApplePaymentMethod
    {
        public string DisplayName { get; set; }
        public string Network { get; set; }
        public string Type { get; set; }
    }

    public class ApplePaySource
    {
        public string Data { get; set; }
        public string Signature { get; set; }
        public ApplePayHeader Header { get; set; }
        public string Version { get; set; }
    }

    public class ApplePayHeader
    {
        public string PublicKeyHash { get; set; }
        public string EphemeralPublicKey { get; set; }
        public string TransactionId { get; set; }
    }
}
