using System.Collections.Generic;

namespace payfurl.sdk.Models.PaymentLink
{
    public class CreatePaymentLink
    {
        public string Title { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public List<string> AllowedPaymentTypes { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ConfirmationMessage { get; set; }
        public string RedirectUrl { get; set; }
        public string CallToAction { get; set; }
        public int? LimitPayments { get; set; }
    }
}