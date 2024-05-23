using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewPaymentMethodBankPayment
    {
        public string ProviderId { get; set; }
        public NewBankPayment BankPaymentInformation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Dictionary<string,string> Metadata { get; set; }
        public bool SetDefault { get; set; }
    }
}