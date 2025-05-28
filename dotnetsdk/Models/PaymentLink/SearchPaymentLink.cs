using System;

namespace payfurl.sdk.Models.PaymentLink
{
    public class SearchPaymentLink
    {
        public int? Limit { get; set; }
        public int? Skip { get; set; }
        public DateTime? AddedAfter { get; set; }
        public DateTime? AddedBefore { get; set; }
        public SortBy? Sort { get; set; }

        public enum SortBy
        {
            None,
            Date,
            Title,
        }
    }
}