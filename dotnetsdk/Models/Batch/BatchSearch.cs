using System;

namespace payfurl.sdk.Models.Batch
{
    public class BatchSearch
    {
        public int? Limit { get; set; }
        public int? Skip { get; set; }
        public string Description { get; set; }
        public DateTime? AddedAfter { get; set; }
        public DateTime? AddedBefore { get; set; }
    }
}