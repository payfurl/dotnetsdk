namespace payfurl.sdk.Models
{
    public class ThreeDsInitiate
    {
        public string AcctNumber { get; set; }
        public string Ds { get; set; }
        public string ProviderId { get; set; }
        public string CardProviderId { get; set; }
    }
}
