namespace payfurl.sdk.Models
{
    public class ThreeDsComplete
    {
        public string ThreeDSServerTransID { get; set; }
        public string AuthenticationValue { get; set; }
        public string ThreeDsVersion { get; set; }
        public string DsTransID { get; set; }
        public string Eci { get; set; }
        public string Xid { get; set; }
    }
}
