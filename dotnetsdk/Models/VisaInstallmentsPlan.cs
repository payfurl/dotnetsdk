using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class VisaInstallmentsPlan
    {
        public string PlanId { get; set; }
        public string PlanIdRef { get; set; }
        public string Name { get; set; }
        public int NumberOfInstallments { get; set; }
        public string InstallmentFrequency { get; set; }
        public List<VisaInstallmentsTermsAndConditions> TermsAndConditions { get; set; }
        public long TotalPlanCost { get; set; }
        public long TotalFees { get; set; }
    }

    public class VisaInstallmentsTermsAndConditions
    {
        public string Url { get; set; }
        public int Version { get; set; }
        public string Text { get; set; }
        public string LanguageCode { get; set; }
    }
}
