namespace payfurl.sdk.Models
{
    public class NewCheckoutCustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public NewCheckoutAddress ShippingAddress { get; set; }
        public NewCheckoutAddress BillingAddress { get; set; }
    }
}
