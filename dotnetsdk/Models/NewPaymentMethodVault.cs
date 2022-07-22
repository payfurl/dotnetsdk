namespace payfurl.sdk.Models
{
    public class NewPaymentMethodVault
    {
        public string ProviderId { get; set; }
        public string VaultId { get; set; }
        public string PaymentMethodId { get; set; }
        public string Ccv { get; set; }
    }
}