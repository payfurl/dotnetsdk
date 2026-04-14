namespace payfurl.sdk.Models
{
    public class ValidateMerchantRequest
    {
        public string ValidationUrl { get; set; }
        public string MerchantIdentifier { get; set; }
        public string DomainName { get; set; }
        public string DisplayName { get; set; }
    }
}
