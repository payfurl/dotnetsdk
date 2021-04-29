﻿namespace payfurl.sdk.Models
{
    public class NewChargeCard
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ProviderId { get; set; }
        public string Reference { get; set; }
        public CardRequestInformation PaymentInformation { get; set;}
    }
}