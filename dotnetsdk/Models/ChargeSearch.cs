using System;

namespace payfurl.sdk.Models
{
    public class ChargeSearch
    {
        public string Reference { get; set; }
        public string ProviderId { get; set; }
        public decimal? AmountGreaterThan { get; set; }
        public decimal? AmountLessThan { get; set; }
        public string CustomerId { get; set; }
        public string Status { get; set; }
        public DateTime? AddedAfter { get; set; }
        public DateTime? AddedBefore { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentType { get; set; }
        public string SortBy { get; set; }
        public int? Limit { get; set; }
        public int? Skip { get; set; }
    }
}
