using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class InfoProvider
    {
        public string ThreeDsProviderId { get; set; }
        public string ThreeDsProviderType { get; set; }
        public Dictionary<string, string> RequiredFields { get; set; }
        public GooglePayBaseInfo GooglePay { get; set; }
    }
}