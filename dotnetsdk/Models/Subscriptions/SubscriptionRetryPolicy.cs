﻿using Newtonsoft.Json;
using payfurl.sdk.Tools;

namespace payfurl.sdk.Models.Subscriptions
{

    public class SubscriptionRetryPolicy
    {
        public int? Maximum { get; set; }
        [JsonConverter(typeof(EnumToStringConverter<SubscriptionRetryInterval>))]
        public SubscriptionRetryInterval Interval { get; set; }
        public int? Frequency { get; set; }
    }
}