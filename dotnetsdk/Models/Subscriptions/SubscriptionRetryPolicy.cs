namespace payfurl.sdk.Models.Subscriptions
{

    public class SubscriptionRetryPolicy
    {
        public int? Maximum { get; set; }
        public SubscriptionRetryInterval Interval { get; set; }
        public int? Frequency { get; set; }

        public enum SubscriptionRetryInterval
        {
            Hour,
            Day
        }
    }
}