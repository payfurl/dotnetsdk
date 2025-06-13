using System.Collections.Generic;

namespace payfurl.sdk.Models.PaymentLink
{
    public class SearchPaymentLinkResult
    {
        public List<PaymentLinkData> PaymentLinks { get; set; }
        public long Count { get; set; }
    }
}