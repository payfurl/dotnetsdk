namespace payfurl.sdk.Models
{
    public class NewChargeToken
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
        public string Token { get; set; }
        public CheckoutTransfer Transfer { get; set; }
        public bool Capture { get; set; } = true;
        public Initiator? Initiator {  get;  set; }
        public WebhookConfig Webhook { get; set; }
    }
}
