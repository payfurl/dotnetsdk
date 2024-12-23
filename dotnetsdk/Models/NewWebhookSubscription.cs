using System.Collections.Generic;
using Newtonsoft.Json;
using payfurl.sdk.Tools;

namespace payfurl.sdk.Models
{
    public class NewWebhookSubscription
    {
        public string Url { get; set; }
        public string Authorization { get; set; }
        [JsonConverter(typeof(ListEnumCamelCaseConverter<WebhookType>))]
        public List<WebhookType> Types { get; set; }
    }
}