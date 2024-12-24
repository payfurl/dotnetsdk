using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class TokenData
    {
        public string GatewayTokenId { get; set; }
        public string TokenId { get; set; }
        public string UserId { get; set; }
        public CardData Card { get; set; }
        public ProviderSummary Provider { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateUsed { get; set; }
        public string PayToStatus { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}