namespace payfurl.sdk.Models
{
    public class NewCheckoutItem
    {
        public string Name { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public string ItemUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
