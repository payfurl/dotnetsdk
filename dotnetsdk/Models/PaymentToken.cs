using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class PaymentToken
    {
        public string TokenId { get; set; }
        public string ProviderId { get; set; }
        public DateTime DateAdded { get; set; }
        public string VaultId { get; set; }
        public string Type { get; set; }
        public string PayToStatus { get; set; }
        public string Ip { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public ClickToPayData ClickToPay { get; set; }
        public string NetworkTokenAuthenticateUrl { get; set; }
        public string NetworkToken { get; set; }
        public Dictionary<string, string> NetworkTokenData { get; set; }
        public Dictionary<string, string> GatewayTokenData { get; set; }
        public CardData Card { get; set; }
    }
}
