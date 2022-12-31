using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewCustomerProviderToken : NewCustomerEmailAndPhoneData
    {
        public string ProviderId { get; set; }
        public string ProviderToken { get; set; }
        public Dictionary<string,string> ProviderTokenData { get; set; }
    }
}