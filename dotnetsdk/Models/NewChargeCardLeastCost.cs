namespace payfurl.sdk.Models
{
    public class NewChargeCardLeastCost
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
        public CardRequestInformation PaymentInformation { get; set;}
        public bool Capture { get; set; } = true;
    }
}
