using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class WebhookSubscriptionData
    {
        public string WebhookSubscriptionId { get; set; }
        public string AccountId { get; set; }
        public string Url { get; set; }
        public string Authorization { get; set; }
        public List<WebhookType> Types { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DateRemoved { get; set; }
    }
}