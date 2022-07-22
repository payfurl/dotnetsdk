using System;

namespace payfurl.sdk.Models
{

    public class CustomerDataSummary
    {
        public string CustomerId { get; set; }
        public string Reference { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateAdded { get; set; }
    }
}