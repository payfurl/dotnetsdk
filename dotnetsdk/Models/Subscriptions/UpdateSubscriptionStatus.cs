using Newtonsoft.Json;
using payfurl.sdk.Tools;

namespace payfurl.sdk.Models.Subscriptions
{
    public class UpdateSubscriptionStatus
    {
        [JsonConverter(typeof(EnumToStringConverter<Subscription.SubscriptionStatus>))]
        public Subscription.SubscriptionStatus Status { get; set; }
    }
}
