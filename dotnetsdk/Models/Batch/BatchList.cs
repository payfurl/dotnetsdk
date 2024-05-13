using System.Collections.Generic;

namespace payfurl.sdk.Models.Batch
{
    public class BatchList
    {
        public int Limit { get; set; }
        public int Skip { get; set; }
        public long Count { get; set; }
        public List<BatchSummary> Batches { get; set; }
    }
}