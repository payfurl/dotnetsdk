using System;

namespace payfurl.sdk.Models
{
    public class CustomerData
    {
        public string CustomerId { get; set; }
        public string Reference { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateAdded { get; set; }
        public PaymentMethodSummary DefaultPaymentMethod { get; set; }
        public string Ip { get; set; }
        public DateTime? DateRemoved { get; set; }
        public Address Address { get; set; }
    }
}
