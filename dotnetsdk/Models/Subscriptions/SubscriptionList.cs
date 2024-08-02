using System.Collections.Generic;

namespace payfurl.sdk.Models.Subscriptions
{
    public class SubscriptionList
    {
        public int Limit { get; set; }
        public int Skip { get; set; }
        public long Count { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}