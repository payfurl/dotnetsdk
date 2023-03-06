using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewCustomerToken : NewCustomerEmailAndPhoneData
    {
        public string Token { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}
