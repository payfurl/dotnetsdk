namespace payfurl.sdk.Models
{
    public class ClickToPayInfo
    {
        public bool TokenAuthenticationEnabled { get; set; } = false;
        public string CorrelationId { get; set; }
        public string MerchantTransactionId { get; set; }
        public string Ccv { get; set; }
        public string DigitalCardId { get; set; }
        public string SrcFlowId { get; set; }
    }
}