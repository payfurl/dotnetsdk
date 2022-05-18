namespace payfurl.sdk.Models
{
    public class NewPaymentMethodCard
    {
        public string ProviderId { get; set; }
        public CardRequestInformation PaymentInformation { get; set; }
    }
}
