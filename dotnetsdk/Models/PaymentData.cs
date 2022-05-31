namespace payfurl.sdk.Models
{
    public class PaymentData
    {
        public string PaymentMethodId { get; set; }
        public CardData Card { get; set; }
        public string Type { get; set; }
        public string ProviderType { get; set; }
        public string Email { get; set; }
    }
}
