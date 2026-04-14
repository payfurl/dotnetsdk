namespace payfurl.sdk.Models
{
    public class FraudRequestPaypalInformation
    {
        public string PayerId { get; set; }
        public string PayerEmail { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string PayerAccountCountry { get; set; }
        public string PaymentId { get; set; }
    }
}
