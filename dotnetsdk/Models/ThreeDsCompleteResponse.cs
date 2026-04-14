namespace payfurl.sdk.Models
{
    public class ThreeDsCompleteResponse
    {
        public AcsRenderingType AcsRenderingType { get; set; }
        public string AcsTransID { get; set; }
        public string AuthenticationType { get; set; }
        public string AuthenticationValue { get; set; }
        public string ChallengeCancel { get; set; }
        public string DsTransID { get; set; }
        public string Eci { get; set; }
        public string InteractionCounter { get; set; }
        public string MessageCategory { get; set; }
        public MessageExtensionInfo[] MessageExtension { get; set; }
        public string MessageType { get; set; }
        public string MessageVersion { get; set; }
        public string ThreeDSRequestorTransID { get; set; }
        public string ThreeDSServerTransID { get; set; }
        public string TransStatus { get; set; }
        public string TransStatusReason { get; set; }
    }
}
