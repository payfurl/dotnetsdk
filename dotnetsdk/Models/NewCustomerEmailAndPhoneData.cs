
namespace payfurl.sdk.Models
{
    public class NewCustomerEmailAndPhoneData : CustomerEmailAndPhoneData
    {
        public string Reference { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ip { get; set; }
        public Address Address { get; set; }
    }
}
