namespace payfurl.sdk.Models
{
    public class NewChargeCheckout
    {
        public string CheckoutId { get; set; }
        public FraudCheckStatus FraudCheck { get; set; }
        public string Descriptor { get; set; }
        public string ExternalId { get; set; }
    }
}
