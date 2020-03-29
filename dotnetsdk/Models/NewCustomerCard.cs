namespace payfurl.sdk.Models
{
    public class NewCustomerCard
    {
        public string Reference { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProviderId { get; set; }
        public CardRequestInformation PaymentInformation { get; set;}
    }
}
