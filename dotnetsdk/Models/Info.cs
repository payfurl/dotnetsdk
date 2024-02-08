using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class Info
    {
        public List<Country> Countries { get; set; }
        public List<CurrencyInformationData> Currencies { get; set; }
        public List<string> Timezones { get; set; }

        public class Country
        {
            public string FriendlyName { get; set; }
            public string Name { get; set; }
            public string TwoLetterCode { get; set; }
            public string ThreeLetterCode { get; set; }
            public string NumericCode { get; set; }
        }
    }
}