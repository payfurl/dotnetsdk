namespace payfurl.sdk.Models
{
    public class ThreeDsAuthenticateResponse
    {
        public string AcsChallengeMandated { get; set; }
        public string AcsOperatorID { get; set; }
        public string AcsReferenceNumber { get; set; }
        public AcsRenderingType AcsRenderingType { get; set; }
        public string AcsSignedContent { get; set; }
        public string AcsTransID { get; set; }
        public string AcsURL { get; set; }
        public string AuthenticationType { get; set; }
        public string AuthenticationValue { get; set; }
        public string CardholderInfo { get; set; }
        public string DsReferenceNumber { get; set; }
        public string DsTransID { get; set; }
        public string Eci { get; set; }
        public MessageExtensionInfo[] MessageExtension { get; set; }
        public string MessageType { get; set; }
        public string MessageVersion { get; set; }
        public string SdkTransID { get; set; }
        public string ThreeDSRequestorTransID { get; set; }
        public string ThreeDSServerTransID { get; set; }
        public string TransStatus { get; set; }
        public string TransStatusReason { get; set; }
        public string ThreeDSMonitorURL { get; set; }
    }
}
