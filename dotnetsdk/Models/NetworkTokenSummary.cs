using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NetworkTokenSummary
    {
        public string NetworkTokenId { get; set; }
        public string SchemeTokenId { get; set; }
        public string ProviderId { get; set; }
        public string ProviderType { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public Dictionary<string, string> SchemeTokenData { get; set; }
        public NetworkTokenAuthenticationMethod AuthenticationMethod { get; set; }
    }
}