namespace payfurl.sdk.Models
{
    public class PayToProviderOptions
    {
        public string ProviderId { get; set; }
        public decimal MinimumAmount { get; set; }
        public string Description { get; set; }    
    }
}