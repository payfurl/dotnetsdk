using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewChargePaymentMethod
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethodId { get; set; }
        public string Reference { get; set; }
        public bool Capture { get; set; } = true;
        public Initiator? Initiator {  get;  set; }
        public WebhookConfig Webhook { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = null;
    }
}
