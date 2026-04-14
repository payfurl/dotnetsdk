namespace payfurl.sdk.Models
{
    public class GetVisPlansData
    {
        public string ProviderId { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
