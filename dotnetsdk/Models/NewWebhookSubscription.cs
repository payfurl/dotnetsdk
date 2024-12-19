using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewWebhookSubscription
    {
        public string Url { get; set; }
        public string Authorization { get; set; }
        public List<string> Types { get; set; }
    }
}