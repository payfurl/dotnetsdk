namespace payfurl.sdk.Models
{
    public class UpdatePaymentMethod
    {
        public UpdatePaymentMethodCardRequestInformation Card { get; set; }
    }

    public class UpdatePaymentMethodCardRequestInformation
    {
        public string ExpiryDate { get; set; }
        public string Cardholder { get; set; }
    }
}