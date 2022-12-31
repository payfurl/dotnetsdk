using System;

namespace payfurl.sdk.Models
{
    public class NewPayToAgreement
    {
        public string PayerName { get; set; }
        public PayIdDetails PayerPayIdDetails { get; set; }
        public string Description { get; set; }
        public int MaximumAmount { get; set; }
        public string ProviderId { get; set; }
        public string Ip { get; set; }

        public class PayIdDetails
        {
            public string PayId { get; set; }
            public string PayIdType { get; set; }
        }
    }
}
