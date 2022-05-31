using System;

namespace payfurl.sdk.Models
{
    public class PaymentMethodSearch
    {
        public DateTime? AddedAfter { get; set; }
        public DateTime? AddedBefore { get; set; }
        public string ProviderId { get; set; }
        public string CustomerId { get; set; }
        public int? Limit { get; set; }
        public int? Skip { get; set; }
        public string PaymentType { get; set; }
        public string Search { get; set; }
        public string SortBy { get; set; }
    }
}
