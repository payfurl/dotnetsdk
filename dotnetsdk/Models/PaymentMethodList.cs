using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class PaymentMethodList
    {
        public int Limit { get; set; }
        public int Skip { get; set; }
        public long Count { get; set; }
        public List<PaymentMethodData> PaymentMethods { get; set; }
    }
}