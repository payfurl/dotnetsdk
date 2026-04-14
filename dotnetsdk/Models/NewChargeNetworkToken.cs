using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewChargeNetworkToken : NewCustomerEmailAndPhoneData
    {
        public string NetworkTokenId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ProviderId { get; set; }
        public bool Capture { get; set; } = true;
        public Order Order { get; set; }
        public string CustomerCode { get; set; }
        public string InvoiceNumber { get; set; }
        public Initiator? Initiator { get; set; }
        public WebhookConfig Webhook { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Descriptor { get; set; }
        public string ThreeDSNotificationUrl { get; set; }
    }
}
