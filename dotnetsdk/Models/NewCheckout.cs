using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewCheckout
    {
        public string ProviderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
        public CheckoutTransfer Transfer { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public NewCheckoutCustomer Customer { get; set; }
        public decimal? ShippingAmount { get; set; }
        public List<NewCheckoutItem> Items { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}
