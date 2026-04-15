using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class ApplePayProviderInfo : ApplePayBaseInfo
    {
        public string ProviderId { get; set; }
        public string ProviderType { get; set; }
        public Dictionary<string, string> ClientParameters { get; set; }
    }
}
