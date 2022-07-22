namespace payfurl.sdk.Models
{
    public class VaultDataWithPCI : VaultData
    {
        public string CardNumber { get; set; }
        public string Ccv { get; set; }
    }
}