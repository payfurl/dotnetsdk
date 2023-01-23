using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class Provider
    {
        public string ProviderId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Environment { get; set; }
        public Dictionary<string, string> AuthenticationParameters { get; set; }
        public string ProviderCountry { get;set; }
        public List<ProviderCostData> CostData { get; set; }
        public List<ProviderVisiblityData> HideConfiguration { get; set; }
        public List<ProviderVisiblityData> RequireConfiguration { get; set; }
        public string FallbackProviderId { get;set; }
        public string Currency { get;set; }
        public string MaxCapability { get; set; }
        public bool HasPartialRefund { get; set; }
        public ProviderInfo FallbackProvider { get; set; }


        public class ProviderCostData
        {
            public bool? IsInternationalCard { get; set; }
            public string CardScheme { get; set; }
            public decimal? TransactionCost { get; set; }
            public decimal? TransactionPercentage { get; set; }
        }

        public class ProviderVisiblityData
        {
            public string Currency { get; set; }
            public decimal? LessThanAmount { get; set; }
            public decimal? GreaterThanAmount { get; set; }
            public bool ApplyCurrencyConversion { get; set; }
        }
        
        public class ProviderInfo
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public bool ApplyCurrencyConversion { get; set; }
        }
    }
}