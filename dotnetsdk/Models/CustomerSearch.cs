using System;

namespace payfurl.sdk.Models
{
    public class CustomerSearch
    {
        public string Reference { get; set; }
        public string Email { get; set; }
        public DateTime? AddedAfter { get; set; }
        public DateTime? AddedBefore { get; set; }
        public string Search { get; set; }
        public int? Limit { get; set; }
        public int? Skip { get; set; }
        public string SortBy { get; set; }
    }
}
