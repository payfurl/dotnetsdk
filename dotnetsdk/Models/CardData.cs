namespace payfurl.sdk.Models
{
    public class CardData
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cardholder { get; set; }
        public string Type { get; set; }
        public IinData IinData { get; set; }
    }
}
