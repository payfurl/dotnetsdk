using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class PaymentMethodData
    {
        public string PaymentMethodId { get; set; }
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public string Type { get; set; }
        public CardData Card { get; set; }
        public string ProviderId { get; set; }
        public string ProviderType { get; set; }
        public DateTime DateAdded { get; set; }
        public string Email { get; set; }
        public string ProviderPaymentMethodId { get; set; }
        public Dictionary<string,string> ProviderPaymentMethodData { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}
