namespace payfurl.sdk.Models
{
    public class NewCustomerPayTo : NewCustomerEmailAndPhoneData
    {
        public NewPayToAgreement PayToAgreement { get; set; }
    }
}