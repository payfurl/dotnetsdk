namespace payfurl.sdk.Models.Batch
{
    public class NewTransactionPaymentMethod
    {
        public int Count { get; set; } 
        public string Description { get; set; }
        public string Batch { get; set; }
        public WebhookConfig WebhookConfig { get; set; }
    }
}