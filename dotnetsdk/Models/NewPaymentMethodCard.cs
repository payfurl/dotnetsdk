using System;

namespace payfurl.sdk.Models
{
    public class NewPaymentMethodCard
    {
        public string ProviderId { get; set; }
        public CardRequestInformation PaymentInformation { get; set; }
        public bool VaultCard { get; set; }
        public DateTime? VaultExpireDate { get; set; }
        public int? VaultExpireSeconds { get; set; }
    }
}
