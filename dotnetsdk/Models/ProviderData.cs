using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class ProviderData
    {
        public string ProviderId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Environment { get; set; }
        public Dictionary<string, string> AuthenticationParameters { get; set; }
        public Dictionary<string, string> AdditionalParameters { get; set; }

        public string ProviderCountry { get;set; }
        public List<ProviderCostDataModel> CostData { get; set; }
        public List<ProviderVisibilityDataModel> HideConfiguration { get; set; }
        public List<ProviderVisibilityDataModel> RequireConfiguration { get; set; }
        public string FallbackProviderId { get;set; }
        public string Currency { get;set; }
        public string MaxCapability { get; set; }
        public bool HasPartialRefund { get; set; }
        public ProviderInfoModel FallbackProvider { get; set; }
        public bool HasThreeDsCapability { get; set; }

        public ProviderData()
        {
            AuthenticationParameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        public class ProviderCostDataModel
        {
            public bool? IsInternationalCard { get; set; }
            public string CardScheme { get; set; }
            public decimal? TransactionCost { get; set; }
            public decimal? TransactionPercentage { get; set; }
        }

        public class ProviderVisibilityDataModel
        {
            public string Currency { get; set; }
            public decimal? LessThanAmount { get; set; }
            public decimal? GreaterThanAmount { get; set; }
            public bool ApplyCurrencyConversion { get; set; }
        }
        
        public class ProviderInfoModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public bool ApplyCurrencyConversion { get; set; }
        }
    }
}