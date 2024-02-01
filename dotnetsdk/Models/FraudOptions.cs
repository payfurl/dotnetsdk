namespace payfurl.sdk.Models
{
    public class FraudOptions
    {
        public string ProviderId { get; set; }
        public string ProviderType { get; set; }
        public bool Enabled { get; set; }
        public bool StopPaymentOnFailure { get; set; }
    }
}