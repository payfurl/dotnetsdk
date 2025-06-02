using System.Collections.Generic;

namespace payfurl.sdk.Models.Token
{
    public class NewTokenPaymentMethod
    {
        public string PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Order Order { get; set; }
        public Address BillingAddress { get; set; }
        public Geolocation Geolocation { get; set; }
        public Dictionary<string, string> AuthenticationData { get; set; }
    }
}