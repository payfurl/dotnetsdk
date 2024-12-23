using System;

namespace payfurl.sdk.Models
{
    public class WebhookSubscriptionSearch
    {
        public string Id { get; set; }
        public int? Limit { get; set; }
        public int? Skip { get; set; }
        public string Type { get; set; }
        public DateTime? AddedAfter { get; set; }
        public DateTime? AddedBefore { get; set; }
        public SortBy Sort { get; set; }

        public enum SortBy
        {
            None,
            Date,
        }
    }
}