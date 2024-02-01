using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class BnplOptions
    {
        public string ProviderId { get; set; }
        public string ProviderType { get; set; }
        public Dictionary<string, string> Options { get; set; }
    }
}