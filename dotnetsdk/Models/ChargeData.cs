using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class ChargeData
    {
        public string ChargeId { get; set; }
        public string ProviderChargeId { get; set; }
        public decimal Amount { get; set; }
        public string ProviderId { get; set; }
        public string Reference { get; set; }
        public PaymentData PaymentInformation { get; set; }
        public string CustomerId { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? SuccessDate { get; set; }
        public DateTime? VoidDate { get; set; }
        public decimal? RefundedAmount { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string EstimatedCostCurrency { get; set; }
        public string Currency { get; set; }
        public List<Refund> Refunds { get; set; }
        public List<FailureData> FailedAttempts { get; set; }
        public CustomerDataSummary Customer { get; set; }
        public ProviderSummary Provider { get; set; }
        public string ThreeDSServerTransID { get; set; }
        public bool ThreeDsVerified { get; set; }
        public decimal? AuthorisationAmount { get; set; }

        public class Refund
        {
            public decimal Amount { get; set; }
            public DateTime DateAdded { get; set; }
        }
    }
}
