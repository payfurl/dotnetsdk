namespace payfurl.sdk.Models
{
    public class NewRefund
    {
        public string ChargeId { get; set; }
        public decimal? RefundAmount { get; set; }
        public string Comment { get; set; }
    }
}
