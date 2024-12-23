using System;
using Newtonsoft.Json;
using payfurl.sdk.Tools;

namespace payfurl.sdk.Models.Subscriptions
{

    public class Subscription
    {
        public string SubscriptionId { get; set; }
        public string PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        [JsonConverter(typeof(EnumToStringConverter<SubscriptionInterval>))]
        public SubscriptionInterval Interval { get; set; }
        public int? Frequency { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public SubscriptionEnd EndAfter { get; set; }
        public SubscriptionRetryPolicy Retry { get; set; }
        public WebhookConfig Webhook { get; set; }
        [JsonConverter(typeof(EnumToStringConverter<SubscriptionStatus>))]
        public SubscriptionStatus Status { get; set; }

<<<<<<< HEAD
        public enum SubscriptionStatus
        {
            Complete,
            Active,
            Cancelled,
            Suspended
        }

        public enum SubscriptionInterval
        {
            Day,
            Week,
            Month,
            Year
        }
=======
>>>>>>> master
    }
}