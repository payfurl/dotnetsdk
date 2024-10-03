using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewChargeBankPayment
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ProviderId { get; set; }
        public string Reference { get; set; }
        public BankPaymentInformationData BankPaymentInformation { get; set; }
        public bool Capture { get; set; } = true;
        public string Ip { get; set; } = null;
        public Address Address { get; set; }
        public Order Order { get; set; }
        public string CustomerCode { get; set; }
        public string InvoiceNumber { get; set; }
        public Initiator? Initiator {  get;  set; }
        public WebhookConfig Webhook { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Descriptor { get; set; }
        public string ThreeDSNotificationUrl { get; set; }
        public Geolocation Geolocation { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
