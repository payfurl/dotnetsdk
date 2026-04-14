namespace payfurl.sdk.Models
{
    public class ThreeDsAuthenticate
    {
        public string AcctID { get; set; }
        public AcctInfoData AcctInfo { get; set; }
        public string AcctNumber { get; set; }
        public string AcctType { get; set; }
        public string AcquirerBIN { get; set; }
        public string AcquirerMerchantID { get; set; }
        public string AddrMatch { get; set; }
        public string BillAddrCity { get; set; }
        public string BillAddrCountry { get; set; }
        public string BillAddrLine1 { get; set; }
        public string BillAddrLine2 { get; set; }
        public string BillAddrLine3 { get; set; }
        public string BillAddrPostCode { get; set; }
        public string BillAddrState { get; set; }
        public string BrowserInfo { get; set; }
        public string BrowserAcceptHeader { get; set; }
        public string BrowserColorDepth { get; set; }
        public string BrowserIP { get; set; }
        public bool BrowserJavaEnabled { get; set; }
        public string BrowserLanguage { get; set; }
        public string BrowserScreenHeight { get; set; }
        public string BrowserScreenWidth { get; set; }
        public string BrowserTZ { get; set; }
        public string BrowserUserAgent { get; set; }
        public string CardExpiryDate { get; set; }
        public string CardholderName { get; set; }
        public string DeviceChannel { get; set; }
        public DeviceRenderOptionsInfo DeviceRenderOptions { get; set; }
        public string Ds { get; set; }
        public string Email { get; set; }
        public PhoneNumber HomePhone { get; set; }
        public string Mcc { get; set; }
        public string MerchantCountryCode { get; set; }
        public string MerchantName { get; set; }
        public MerchantRiskIndicatorInfo MerchantRiskIndicator { get; set; }
        public string MessageCategory { get; set; }
        public MessageExtensionInfo[] MessageExtension { get; set; }
        public string MessageType { get; set; }
        public string MessageVersion { get; set; }
        public PhoneNumber MobilePhone { get; set; }
        public string NotificationURL { get; set; }
        public string PurchaseAmount { get; set; }
        public string PurchaseCurrency { get; set; }
        public string PurchaseDate { get; set; }
        public string PurchaseExponent { get; set; }
        public string PurchaseInstalData { get; set; }
        public bool PayTokenInd { get; set; }
        public string RecurringExpiry { get; set; }
        public string RecurringFrequency { get; set; }
        public string SdkAppID { get; set; }
        public string SdkEncData { get; set; }
        public string SdkEphemPubKey { get; set; }
        public string SdkMaxTimeout { get; set; }
        public string SdkReferenceNumber { get; set; }
        public string SdkTransID { get; set; }
        public string ShipAddrCity { get; set; }
        public string ShipAddrCountry { get; set; }
        public string ShipAddrLine1 { get; set; }
        public string ShipAddrLine2 { get; set; }
        public string ShipAddrLine3 { get; set; }
        public string ShipAddrPostCode { get; set; }
        public string ShipAddrState { get; set; }
        public string ThreeDSCompInd { get; set; }
        public string ThreeDSRequestorAuthenticationInd { get; set; }
        public ThreeDSRequestorAuthenticationInfoData ThreeDSRequestorAuthenticationInfo { get; set; }
        public string ThreeDSRequestorChallengeInd { get; set; }
        public ThreeDSRequestorPriorAuthenticationInfoData ThreeDSRequestorPriorAuthenticationInfo { get; set; }
        public string ThreeDSRequestorURL { get; set; }
        public string ThreeDSRequestorTransID { get; set; }
        public string ThreeDSServerTransID { get; set; }
        public string ThreeRIInd { get; set; }
        public string TransType { get; set; }
        public PhoneNumber WorkPhone { get; set; }
        public string ThreeDSAuthURL { get; set; }
        public string Ccv { get; set; }

        public class AcctInfoData
        {
            public string ChAccAgeInd { get; set; }
            public string ChAccChange { get; set; }
            public string ChAccChangeInd { get; set; }
            public string ChAccDate { get; set; }
            public string ChAccPwChange { get; set; }
            public string ChAccPwChangeInd { get; set; }
            public string NbPurchaseAccount { get; set; }
            public string PaymentAccAge { get; set; }
            public string PaymentAccInd { get; set; }
            public string ProvisionAttemptsDay { get; set; }
            public string ShipAddressUsage { get; set; }
            public string ShipAddressUsageInd { get; set; }
            public string ShipNameIndicator { get; set; }
            public string SuspiciousAccActivity { get; set; }
            public string TxnActivityDay { get; set; }
            public string TxnActivityYear { get; set; }
        }

        public class DeviceRenderOptionsInfo
        {
            public string SdkInterface { get; set; }
            public string[] SdkUiType { get; set; }
        }

        public class PhoneNumber
        {
            public string Cc { get; set; }
            public string Subscriber { get; set; }
        }

        public class ThreeDSRequestorAuthenticationInfoData
        {
            public string ThreeDSReqAuthData { get; set; }
            public string ThreeDSReqAuthMethod { get; set; }
            public string ThreeDSReqAuthTimestamp { get; set; }
        }

        public class ThreeDSRequestorPriorAuthenticationInfoData
        {
            public string ThreeDSReqPriorAuthData { get; set; }
            public string ThreeDSReqPriorAuthMethod { get; set; }
            public string ThreeDSReqPriorAuthTimestamp { get; set; }
            public string ThreeDSReqPriorRef { get; set; }
        }

        public class MerchantRiskIndicatorInfo
        {
            public string DeliveryEmailAddress { get; set; }
            public string DeliveryTimeframe { get; set; }
            public string GiftCardAmount { get; set; }
            public string GiftCardCount { get; set; }
            public string GiftCardCurr { get; set; }
            public string PreOrderDate { get; set; }
            public string PreOrderPurchaseInd { get; set; }
            public string ReorderItemsInd { get; set; }
            public string ShipIndicator { get; set; }
        }
    }
}
