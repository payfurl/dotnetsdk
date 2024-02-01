using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class InfoProviders
    {
        public List<string> PaypalProviders { get; set; }
        public bool HasCardProviders { get; set; }
        public bool HasPaypalProviders { get; set; }
        public string Required3dsProvider { get; set; }
        public bool HasPayToProviders { get; set; }
        public List<string> PayToProviders { get; set; }
        public List<PayToProviderOptions> PayToProvidersWithOptions { get; set; }
        public bool HasBnplProviders { get; set; }
        public List<BnplOptions> BnplProviders { get; set; }
        public bool HasVisaInstallmentsProviders { get; set; }
        public List<string> VisaInstallmentsProviders { get; set; }
        public bool HasFraudProviders { get; set; }
        public List<FraudOptions> FraudProviders { get; set; }
        public bool HasThreeDsProviders { get; set; }
        public List<ThreeDsOptions> ThreeDsProviders { get; set; }
        public bool HasGooglePayProviders { get; set; }
        public List<GooglePayProviderInfo> GooglePayProviders { get; set; }
        public bool ClickToPayEnabled { get; set; }
        public ClickToPayOptions ClickToPayOptions { get; set; }
    }
}