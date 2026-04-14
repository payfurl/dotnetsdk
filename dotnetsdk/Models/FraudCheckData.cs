namespace payfurl.sdk.Models
{
    public class FraudCheckData
    {
        public string SiteId { get; set; }
        public string ProviderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public FraudRequestCardInformation PaymentInformation { get; set; }
        public FraudRequestPaypalInformation PaypalInformation { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public Address Address { get; set; }
        public Order Order { get; set; }
        public ConnectionInformation ConnectionInformation { get; set; }
    }
}
