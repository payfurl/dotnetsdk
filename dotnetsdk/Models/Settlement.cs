using System;

namespace payfurl.sdk.Models
{
    public class Settlement
    {
        public string SettlementId { get; set; }
        public DateTime SettlementDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProviderId { get; set; }
        public string ProviderName { get; set; }
        public string ChargeId { get; set; }
        public decimal Amount { get; set; }
    }
}
