using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class WebhookTransaction
    {
        public WebhookMeta Meta { get; set; }
        public WebhookTransactionData Data { get; set; }
    }

    public class WebhookMeta
    {
        public string MessageId { get; set; }
        public string Timestamp { get; set; }
        public string Type { get; set; }
        public string EventType { get; set; }
    }

    public class WebhookTransactionData
    {
        public string ChargeId { get; set; }
        public string ProviderChargeId { get; set; }
        public decimal Amount { get; set; }
        public string ProviderId { get; set; }
        public string Reference { get; set; }
        public WebhookPaymentData PaymentInformation { get; set; }
        public string CustomerId { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? SuccessDate { get; set; }
        public DateTime? VoidDate { get; set; }
        public decimal? RefundedAmount { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string EstimatedCostCurrency { get; set; }
        public string Currency { get; set; }
        public List<WebhookRefund> Refunds { get; set; }
        public string ThreeDSServerTransID { get; set; }
        public bool ThreeDsVerified { get; set; }
        public decimal? AuthorisationAmount { get; set; }
        public string Initiator { get; set; }

        public class WebhookRefund
        {
            public decimal Amount { get; set; }
            public DateTime DateAdded { get; set; }
            public string Comment { get; set; }
        }

        public class WebhookPaymentData
        {
            public string PaymentMethodId { get; set; }
            public WebhookCardData Card { get; set; }
            public string Type { get; set; }
        }

        public class WebhookCardData
        {
            public string CardNumber { get; set; }
            public string ExpiryDate { get; set; }
            public string Type { get; set; }
            public string CardType { get; set; }
            public string CardIin { get; set; }
        }
    }
}
