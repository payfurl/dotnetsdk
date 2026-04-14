namespace payfurl.sdk.Models
{
    public class FraudRequestCardInformation
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cardholder { get; set; }
        public string ThreeDSServerTransID { get; set; }
    }
}
