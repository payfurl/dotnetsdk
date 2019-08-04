namespace evertech.sdk.Models
{
    public class PaymentData
    {
        public CardData Card { get; set; }
        public string Type { get; set; }
        public string ProviderType { get; set; }
        public string Email { get; set; }
    }
}
