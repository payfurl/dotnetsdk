using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models.Token
{
    public class NewTokenCard
    {
        public string ProviderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public bool CreateNetworkToken { get; set; }
        public CardRequestInformation PaymentInformation { get; set; }
        public bool VaultCard { get; set; } = true;
        public DateTime? VaultExpireDate { get; set; }
        public int? VaultExpireSeconds { get; set; }
        public string Ip { get; set; } = null;
        public string UserAgent { get; set; } = null;
        public VisaInstallmentsInfo VisaInstallments { get; set; }
        public FraudCheckStatus FraudCheck { get; set; }
        public ClickToPayInfo ClickToPay { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public bool Verify { get; set; } = false;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Order Order { get; set; }
        public Address BillingAddress { get; set; }
        public Geolocation Geolocation { get; set; }
    }
}