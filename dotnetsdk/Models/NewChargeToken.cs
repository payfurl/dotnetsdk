﻿namespace payfurl.sdk.Models
{
    public class NewChargeToken
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
        public string Token { get; set; }
    }
}
