namespace evertech.sdk.Models
{
    public class CustomerSearch
    {
        public string PaymentMethodId { get; set; }
        public string Reference { get; set; }
        public int? Limit { get; set; }
        public int? Skip { get; set; }
    }
}
