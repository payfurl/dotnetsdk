using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using payfurl.sdk.Tools;

namespace payfurl.sdk.Models.Subscriptions
{

    public class SubscriptionCreate
    {
        public string PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        [JsonConverter(typeof(EnumToStringConverter<SubscriptionInterval>))]
        public SubscriptionInterval Interval { get; set; }
        public int? Frequency { get; set; }
        public DateTime? StartDate { get; set; }
        public SubscriptionEnd EndAfter { get; set; }
        public SubscriptionRetryPolicy Retry { get; set; }
        public WebhookConfig Webhook { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}