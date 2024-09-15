using System;

namespace payfurl.sdk.Models
{
    public class ChargeSearch
    {
        public string PaymentMethodId { get; set; }
        public string Reference { get; set; }
        public decimal? AmountGreaterThan { get; set; }
        public decimal? AmountLessThan { get; set; }
        public string CustomerId { get; set; }
        public string Status { get; set; }
        public DateTime? AddedAfter { get; set; }
        public DateTime? AddedBefore { get; set; }
        public string ProviderId { get; set; }
        public string PaymentType { get; set; }
        public string CardType { get; set; }
        public string Currency { get; set; }
        public string CardNumber { get; set; }
        public string Cardholder { get; set; }
        public int? Limit { get; set; }
        public int? Skip { get; set; }
        public string SortBy { get; set; }
    }
}
