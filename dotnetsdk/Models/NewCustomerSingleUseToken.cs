using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewCustomerSingleUseToken : NewCustomerEmailAndPhoneData
    {
        public string ProviderId { get; set; }
        public string ProviderToken { get; set; }
        public Dictionary<string,string> ProviderTokenData { get; set; }
        public Dictionary<string,string> Metadata { get; set; }
    }
}