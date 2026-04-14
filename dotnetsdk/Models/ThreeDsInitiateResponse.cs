namespace payfurl.sdk.Models
{
    public class ThreeDsInitiateResponse
    {
        public string AcsStartProtocolVersion { get; set; }
        public string AcsEndProtocolVersion { get; set; }
        public string DsStartProtocolVersion { get; set; }
        public string DsEndProtocolVersion { get; set; }
        public string ThreeDSRequestorTransID { get; set; }
        public string ThreeDSServerTransID { get; set; }
        public string ThreeDSAuthURL { get; set; }
        public string ThreeDSMethodURL { get; set; }
        public string ThreeDSMonitorURL { get; set; }
        public bool ThreeDSMethodAvailable { get; set; }
        public string ThreeDsServerCallbackUrl { get; set; }
    }
}
