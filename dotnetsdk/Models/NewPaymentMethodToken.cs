using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewPaymentMethodToken
    {
        public string Token { get; set; }
        public bool SetDefault { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}
