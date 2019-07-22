using System;

namespace evertech.sdk.Models
{
    public class ChargeData
    {
        public string ChargeId { get; set; }
        public string ProviderChangeId { get; set; }
        public decimal Amount { get; set; }
        public string ProviderId { get; set; }
        public string Reference { get; set; }
        public PaymentData PaymentInformation { get; set; }
        public string CustomerId { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
