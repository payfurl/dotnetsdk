using System.Collections.Generic;

namespace payfurl.sdk.Models.Dispute
{
    public class DisputeList
    {
        public int Limit { get; set; }
        public int Skip { get; set; }
        public long Count { get; set; }
        public List<DisputeData> Disputes { get; set; }
    }
}
