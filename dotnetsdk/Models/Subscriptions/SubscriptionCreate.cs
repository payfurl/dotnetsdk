using System;

namespace payfurl.sdk.Models.Subscriptions
{

    public class SubscriptionCreate
    {
        public string PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public Subscription.SubscriptionInterval Interval { get; set; }
        public int? Frequency { get; set; }
        public DateTime? StartDate { get; set; }
        public SubscriptionEnd EndAfter { get; set; }
        public SubscriptionRetryPolicy Retry { get; set; }
        public WebhookConfig Webhook { get; set; }
    }
}