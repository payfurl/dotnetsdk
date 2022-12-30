using System;

namespace payfurl.sdk.Models
{
    public class UpdateCustomer : CustomerEmailAndPhoneData
    {
        public Address Address { get; set; }
    }
}
