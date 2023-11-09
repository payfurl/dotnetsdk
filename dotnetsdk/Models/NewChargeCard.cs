using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewChargeCard
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ProviderId { get; set; }
        public string Reference { get; set; }
        public CardRequestInformation PaymentInformation { get; set;}
        public bool Capture { get; set; } = true;
        public Initiator? Initiator {  get;  set; }
        public WebhookConfig Webhook { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = null;
        public string Descriptor { get; set; }
    }
}
