using System;

namespace payfurl.sdk.Models.Subscriptions
{

    public class Subscription
    {
        public string SubscriptionId { get; set; }
        public string PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public SubscriptionInterval Interval { get; set; }
        public int? Frequency { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public SubscriptionEnd EndAfter { get; set; }
        public SubscriptionRetryPolicy Retry { get; set; }
        public WebhookConfig Webhook { get; set; }
        public SubscriptionStatus Status { get; set; }

        public enum SubscriptionStatus
        {
            Complete,
            Active,
            Cancelled
        }

        public enum SubscriptionInterval
        {
            Day,
            Week,
            Month,
            Year
        }
    }
}