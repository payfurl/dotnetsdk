using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class SingleUseToken : NewCustomerEmailAndPhoneData
    {
        public string ProviderId { get; set; }
        public string ProviderToken { get; set; }
        public Dictionary<string, string> ProviderTokenData { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string FallbackPaymentMethodId { get; set; }
        public WebhookConfig Webhook { get; set; }
    }
}
