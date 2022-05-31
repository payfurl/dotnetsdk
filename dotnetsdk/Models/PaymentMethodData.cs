﻿using System;

namespace payfurl.sdk.Models
{
    public class PaymentMethodData
    {
        public string PaymentMethodId { get; set; }
        public string CustomerId { get; set; }
        public string Type { get; set; }
        public CardData Card { get; set; }
        public string ProviderId { get; set; }
        public string ProviderType { get; set; }
        public DateTime DateAdded { get; set; }
        public string Email { get; set; }
        public CustomerDataSummary Customer { get; set; }
        public ProviderSummary Provider { get; set; }
    }
}
