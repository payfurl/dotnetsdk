using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class WebhookSubscriptionSearchResults
    {
        public List<WebhookSubscriptionData> WebhookSubscriptions { get; set; }
        public long Count { get; set; }
    }
}