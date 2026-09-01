using System;

namespace payfurl.sdk.Models.Dispute
{
    public class DisputeSearch
    {
        public int? Limit { get; set; }
        public int? Skip { get; set; }
        public string TransactionId { get; set; }
        public string ProviderId { get; set; }
        public string Status { get; set; }
        public string Stage { get; set; }
        public string ReasonCategory { get; set; }
        public string Currency { get; set; }
        public decimal? AmountGreaterThan { get; set; }
        public decimal? AmountLessThan { get; set; }
        public DateTime? AddedAfter { get; set; }
        public DateTime? AddedBefore { get; set; }
        public DateTime? DueBefore { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
