namespace payfurl.sdk.Models
{
    public class FraudCheckStatus
    {
        public string Decision { get; set; }
        public string ProviderId { get; set; }
        public string TransactionId { get; set; }
    }
}