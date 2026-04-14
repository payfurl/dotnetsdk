using System.Collections.Generic;
using System;

namespace payfurl.sdk.Models
{
    public class NewProvider
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Environment { get; set; }
        public Dictionary<string, string> AuthenticationParameters { get; set; }
        public Dictionary<string, string> AdditionalParameters { get; set; }
        public string ProviderCountry { get;set; }
        public string Currency { get; set; }

        public NewProvider()
        {
            AuthenticationParameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            AdditionalParameters = new Dictionary<string, string>();
        }
    }
}
