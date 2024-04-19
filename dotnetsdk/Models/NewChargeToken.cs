using System.Collections.Generic;

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
        public Dictionary<string, string> Metadata { get; set; } = null;
        public string Descriptor { get; set; }
        public string ThreeDSNotificationUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
