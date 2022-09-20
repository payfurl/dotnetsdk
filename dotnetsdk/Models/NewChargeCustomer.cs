﻿namespace payfurl.sdk.Models
{
    public class NewChargeCustomer
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CustomerId { get; set; }
        public string Reference { get; set; }
        public bool Capture { get; set; } = true;
    }
}
