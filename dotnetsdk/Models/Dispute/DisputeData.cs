using System;

namespace payfurl.sdk.Models.Dispute
{
    public class DisputeData
    {
        public string DisputeId { get; set; }
        public string TransactionId { get; set; }
        public string ProviderId { get; set; }
        public string Type { get; set; }
        public string GatewayDisputeId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public decimal? Fee { get; set; }
        public string Status { get; set; }
        public string Stage { get; set; }
        public string ReasonCategory { get; set; }
        public string Reason { get; set; }
        public string NetworkReasonCode { get; set; }
        public string CardBrand { get; set; }
        public DateTime DisputeDate { get; set; }
        public DateTime? EvidenceDueBy { get; set; }
        public int? SubmissionCount { get; set; }
        public bool? IsChargeRefundable { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
