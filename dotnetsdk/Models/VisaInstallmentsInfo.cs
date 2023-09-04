using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class VisaInstallmentsInfo
    {
        public string ProviderId { get; set; }
        public string VisPlanId { get; set; }
        public string VisPlanIdRef { get; set; }
        public int VisAcceptedTAndCVersion { get; set; }
        public string VisPlanName { get; set; }
        public int VisNumberOfInstallments { get; set; }
        public decimal VisTotalFees { get; set; }
        public decimal VisTotalPlanCost { get; set; }
        public string VisInstallmentFrequency { get; set; }
        public List<VisaInstallmentsPlanTermsAndConditionsType> VisTermsAndConditions { get; set; }

        public class VisaInstallmentsPlanTermsAndConditionsType 
        {
            public string Url { get; set; }
            public int Version { get; set; }
            public string Text { get; set; }
            public string LanguageCode { get; set; }
        }    }
}