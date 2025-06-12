using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class PaymentMethodData
    {
        public string PaymentMethodId { get; set; }
        public string CustomerId { get; set; }
        public string Type { get; set; }
        public CardData Card { get; set; }
        public BankPaymentData BankPayment { get; set; }
        public string ProviderId { get; set; }
        public string ProviderType { get; set; }
        public DateTime DateAdded { get; set; }
        public string Email { get; set; }
        public CustomerDataSummary Customer { get; set; }
        public ProviderSummary Provider { get; set; }
        public PayToSummary PayTo { get; set; }
        public NetworkTokenSummary NetworkToken { get; set; }
        public string VaultId { get; set; }
        public string PayToStatus { get; set; }
        public string Ip { get; set; }
        public DateTime? DateRemoved { get; set; }
        public string ProviderPaymentMethodId { get; set; }
        public Dictionary<string,string> ProviderPaymentMethodData { get; set; }
        public string Token { get; set; }
        public string NetworkTokenId { get; set; }
        public string FallbackPaymentMethodId { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}
