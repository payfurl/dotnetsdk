using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class UpdateProvider
    {
        public string Name { get; set; }
        public Dictionary<string, string> AuthenticationParameters { get; set; }
        public string ProviderCountry { get;set; }
        public string Currency { get; set; }

        public UpdateProvider()
        {
            AuthenticationParameters = new Dictionary<string, string>();
        }
    }
}