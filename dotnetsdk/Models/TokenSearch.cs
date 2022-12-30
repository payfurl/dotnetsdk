using System;

namespace payfurl.sdk.Models
{
    public class TokenSearch
    {
        public string ProviderId { get; set; }
        public DateTime? AddedAfter { get; set; }
        public DateTime? AddedBefore { get; set; }
        public string SortBy { get; set; }
        public string Status { get; set; }
        public int? Limit { get; set; }
        public int? Skip { get; set; }
    }
}
