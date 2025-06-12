namespace payfurl.sdk.Models
{
    public class PayToSummary
    {
        public string PayToId { get; set; }
        public string Status { get; set; }
        public string PayerName { get; set; }
        public PayerPayIdDetails PayerPayIdDetails { get; set; }
        public DirectDebitDetails PayerDirectDebitDetails { get; set; }
        public string ProviderId { get; set; }
        public string ProviderType { get; set; }
    }

    public class PayerPayIdDetails
    {
        public string PayId { get; set; }
        public string PayIdType { get; set; }
    }
    
    public class DirectDebitDetails
    {
        public string Bsb { get; set; }
        public string AccountNumber { get; set; }
    }
}