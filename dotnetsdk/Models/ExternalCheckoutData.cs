using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class ExternalCheckoutData
    {
        public string CheckoutId { get; set; }
        public string ExternalId { get; set; }
        public string AccountId { get; set; }
        public string ProviderId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string TransactionId { get; set; }
        public string TokenId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? SuccessDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
        public PaymentData PaymentInformation { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public NewCheckoutCustomer Customer { get; set; }
        public decimal ShippingAmount { get; set; }
        public List<NewCheckoutItem> Items { get; set; }
    }
}
