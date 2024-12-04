using System.Collections.Generic;

namespace payfurl.sdk.Models.Subscriptions
{

    public class SubscriptionUpdate
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Interval { get; set; }
        public int? Frequency { get; set; }
        public SubscriptionEnd EndAfter { get; set; }
        public SubscriptionRetryPolicy Retry { get; set; }
        public WebhookConfig Webhook { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}