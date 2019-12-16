using System;

namespace evertech.sdk.Models
{
    public class CustomerSearch
    {
        public string Reference { get; set; }
        public string PaymentMethodId { get; set; }
        public string CustomerId { get; set; }
        public string Email { get; set; }
        public DateTime? AddedAfter { get; set; }
        public DateTime? AddedBefore { get; set; }
        public string Search { get; set; }
        public int? Limit { get; set; }
        public int? Skip { get; set; }
    }
}
