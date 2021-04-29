using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class CustomerList
    {
        public int Limit { get; set; }
        public int Skip { get; set; }
        public int Count { get; set; }
        public List<CustomerData> Customers { get; set; }
    }
}
