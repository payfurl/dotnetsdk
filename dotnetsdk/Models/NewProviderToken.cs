using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewProviderToken
    {
        public string ProviderId { get; set; }
        public string ProviderToken { get; set; }
        public string Email { get; set; }
        public Dictionary<string,string> ProviderTokenData { get; set; }
        public Dictionary<string,string> Metadata { get; set; }
        public bool Verify { get; set; }
    }
}