namespace payfurl.sdk.Models
{
    public class NewChargePaymentMethod
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethodId { get; set; }
        public string Reference { get; set; }
    }
}
