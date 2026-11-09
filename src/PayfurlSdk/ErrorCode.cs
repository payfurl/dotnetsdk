namespace Payfurl.Sdk;

/// <summary>
/// The API's error codes (<c>code</c> in an error response, <see cref="ApiException.ErrorCode"/>): the API's
/// <c>ErrorCodeType</c> (services/_shared/src/common/Errors/ErrorCodeType.cs). A code this version of the SDK does not
/// know yet is still its number: <c>(int)exception.ErrorCode</c>. Each code is documented at
/// https://docs.payfurl.com/errorcodes, and <see cref="ApiException.Type"/> links its entry.
/// </summary>
public enum ErrorCode
{
    /// <summary>NoError</summary>
    NoError = 0,
    /// <summary>UnknownError</summary>
    UnknownError = 1,
    /// <summary>InvalidAmount</summary>
    InvalidAmount = 2,
    /// <summary>InvalidAuthoriseDetails</summary>
    InvalidAuthoriseDetails = 3,
    /// <summary>InvalidCaptureDetails</summary>
    InvalidCaptureDetails = 4,
    /// <summary>InvalidCardNumber</summary>
    InvalidCardNumber = 5,
    /// <summary>InvalidCcv</summary>
    InvalidCcv = 6,
    /// <summary>InvalidCountry</summary>
    InvalidCountry = 7,
    /// <summary>InvalidCountryForTransfer</summary>
    InvalidCountryForTransfer = 8,
    /// <summary>InvalidExpiryDate</summary>
    InvalidExpiryDate = 9,
    /// <summary>InvalidPaymentDetails</summary>
    InvalidPaymentDetails = 10,
    /// <summary>InvalidPaypalAccount</summary>
    InvalidPaypalAccount = 11,
    /// <summary>InvalidRefundDetails</summary>
    InvalidRefundDetails = 12,
    /// <summary>InvalidVoidDetails</summary>
    InvalidVoidDetails = 13,
    /// <summary>ExpiredCard</summary>
    ExpiredCard = 14,
    /// <summary>InsufficientFunds</summary>
    InsufficientFunds = 15,
    /// <summary>LocationNotSupported</summary>
    LocationNotSupported = 16,
    /// <summary>PaypalBillingAgreementCancelled</summary>
    PaypalBillingAgreementCancelled = 17,
    /// <summary>ThreeDSecureFailed</summary>
    ThreeDSecureFailed = 18,
    /// <summary>AccountEmailUnconfirmed</summary>
    AccountEmailUnconfirmed = 19,
    /// <summary>AccountError</summary>
    AccountError = 20,
    /// <summary>AccountIsLockedOrClosed</summary>
    AccountIsLockedOrClosed = 21,
    /// <summary>AccountNotAuthorised</summary>
    AccountNotAuthorised = 22,
    /// <summary>AccountRestricted</summary>
    AccountRestricted = 23,
    /// <summary>AmountExceedsMaxAmountForPayToAgreement</summary>
    AmountExceedsMaxAmountForPayToAgreement = 24,
    /// <summary>AmountTooLarge</summary>
    AmountTooLarge = 25,
    /// <summary>AuthenticationAuthorizationError</summary>
    AuthenticationAuthorizationError = 26,
    /// <summary>BillingPostalCodeNotSupported</summary>
    BillingPostalCodeNotSupported = 27,
    /// <summary>CurrencyComplianceIssuesWithTransactions</summary>
    CurrencyComplianceIssuesWithTransactions = 28,
    /// <summary>CurrencyNotSupported</summary>
    CurrencyNotSupported = 29,
    /// <summary>Duplicate</summary>
    Duplicate = 30,
    /// <summary>DuplicateTransaction</summary>
    DuplicateTransaction = 31,
    /// <summary>FeatureUnavailable</summary>
    FeatureUnavailable = 32,
    /// <summary>PayToAgreementIsNotActive</summary>
    PayToAgreementIsNotActive = 33,
    /// <summary>ProviderServiceError</summary>
    ProviderServiceError = 34,
    /// <summary>RateLimitingApplied</summary>
    RateLimitingApplied = 35,
    /// <summary>ReceiverAccountLocked</summary>
    ReceiverAccountLocked = 36,
    /// <summary>RetryLater</summary>
    RetryLater = 37,
    /// <summary>SenderAccountIssue</summary>
    SenderAccountIssue = 38,
    /// <summary>InvalidProviderId</summary>
    InvalidProviderId = 39,
    /// <summary>InvalidAuthentication</summary>
    InvalidAuthentication = 40,
    /// <summary>InvalidEnvironment</summary>
    InvalidEnvironment = 41,
    /// <summary>TokeniseCardError</summary>
    TokeniseCardError = 42,
    /// <summary>TransactionError</summary>
    TransactionError = 43,
    /// <summary>NotSupportCharge</summary>
    NotSupportCharge = 44,
    /// <summary>UnableToRefund</summary>
    UnableToRefund = 45,
    /// <summary>UnableToCharge</summary>
    UnableToCharge = 46,
    /// <summary>UnableToCapture</summary>
    UnableToCapture = 47,
    /// <summary>UnableToVoid</summary>
    UnableToVoid = 48,
    /// <summary>UnableToAddCustomer</summary>
    UnableToAddCustomer = 49,
    /// <summary>NoTransactionFound</summary>
    NoTransactionFound = 50,
    /// <summary>InvalidOrExpiredToken</summary>
    InvalidOrExpiredToken = 51,
    /// <summary>InvalidCustomerId</summary>
    InvalidCustomerId = 52,
    /// <summary>InvalidCharge</summary>
    InvalidCharge = 53,
    /// <summary>InvalidChargeStatus</summary>
    InvalidChargeStatus = 54,
    /// <summary>InvalidCardLength</summary>
    InvalidCardLength = 55,
    /// <summary>InvalidPaymentMethodId</summary>
    InvalidPaymentMethodId = 56,
    /// <summary>InvalidPaymentMethod</summary>
    InvalidPaymentMethod = 57,
    /// <summary>InvalidPaymentType</summary>
    InvalidPaymentType = 58,
    /// <summary>InvalidThreeDSServerTransID</summary>
    InvalidThreeDSServerTransID = 59,
    /// <summary>InvalidTransferAmount</summary>
    InvalidTransferAmount = 60,
    /// <summary>InvalidReferenceId</summary>
    InvalidReferenceId = 61,
    /// <summary>InvalidVaultId</summary>
    InvalidVaultId = 62,
    /// <summary>NoCardProvidersConfigured</summary>
    NoCardProvidersConfigured = 63,
    /// <summary>CustomerHasNoDefaultPaymentMethodId</summary>
    CustomerHasNoDefaultPaymentMethodId = 64,
    /// <summary>NoProviderFound</summary>
    NoProviderFound = 65,
    /// <summary>NoVaultIdAssociatedWithThisPaymentMethod</summary>
    NoVaultIdAssociatedWithThisPaymentMethod = 66,
    /// <summary>NoTransferInformationProvided</summary>
    NoTransferInformationProvided = 67,
    /// <summary>TransferLargerThanOriginalCharge</summary>
    TransferLargerThanOriginalCharge = 68,
    /// <summary>DuplicateGroupReference</summary>
    DuplicateGroupReference = 69,
    /// <summary>DuplicateTransferReference</summary>
    DuplicateTransferReference = 70,
    /// <summary>FeatureAuthoriseAndCaptureIsNotSupported</summary>
    FeatureAuthoriseAndCaptureIsNotSupported = 71,
    /// <summary>InvalidCustomerIdRemoved</summary>
    InvalidCustomerIdRemoved = 72,
    /// <summary>InvalidPaymentMethodIdRemoved</summary>
    InvalidPaymentMethodIdRemoved = 73,
    /// <summary>InvalidAmountMustBePositive</summary>
    InvalidAmountMustBePositive = 74,
    /// <summary>FeatureVerifyPendingIsNotSupported</summary>
    FeatureVerifyPendingIsNotSupported = 75,
    /// <summary>InvalidAmountMaximumAvailableRefund</summary>
    InvalidAmountMaximumAvailableRefund = 76,
    /// <summary>InvalidCaptureAmountPassed</summary>
    InvalidCaptureAmountPassed = 77,
    /// <summary>FeaturePartialCaptureIsNotSupportedCaptureAmount</summary>
    FeaturePartialCaptureIsNotSupportedCaptureAmount = 78,
    /// <summary>AmountOutsideRangeForMerchant</summary>
    AmountOutsideRangeForMerchant = 79,
    /// <summary>GatewayTimeout</summary>
    GatewayTimeout = 80,
    /// <summary>InvalidPaymentInformation</summary>
    InvalidPaymentInformation = 81,
    /// <summary>CannotProvideBothExpireDateAndExpireSeconds</summary>
    CannotProvideBothExpireDateAndExpireSeconds = 82,
    /// <summary>InvalidExpireDateOrExpireSeconds</summary>
    InvalidExpireDateOrExpireSeconds = 83,
    /// <summary>InvalidChargeId</summary>
    InvalidChargeId = 84,
    /// <summary>CannotProvideBothVaultExpireDateAndVaultExpireSeconds</summary>
    CannotProvideBothVaultExpireDateAndVaultExpireSeconds = 85,
    /// <summary>InvalidVaultExpiryDate</summary>
    InvalidVaultExpiryDate = 86,
    /// <summary>InvalidTokenId</summary>
    InvalidTokenId = 87,
    /// <summary>InvalidTransferId</summary>
    InvalidTransferId = 88,
    /// <summary>InvalidNotificationFormat</summary>
    InvalidNotificationFormat = 89,
    /// <summary>ValidationError</summary>
    ValidationError = 90,
    /// <summary>TokenVerificationNotSupported</summary>
    TokenVerificationNotSupported = 91,
    /// <summary>InvalidAddress</summary>
    InvalidAddress = 92,
    /// <summary>CardBlocked</summary>
    CardBlocked = 93,
    /// <summary>Timeout</summary>
    Timeout = 94,
    /// <summary>VisIsNotSupported</summary>
    VisIsNotSupported = 95,
    /// <summary>TheOrderInformationIsInvalid</summary>
    TheOrderInformationIsInvalid = 96,
    /// <summary>MetadataDoesNotContainRequiredFields</summary>
    MetadataDoesNotContainRequiredFields = 97,
    /// <summary>FraudIsNotSupported</summary>
    FraudIsNotSupported = 98,
    /// <summary>CardIsNotEnrolledForVis</summary>
    CardIsNotEnrolledForVis = 99,
    /// <summary>ProviderNotSupported</summary>
    ProviderNotSupported = 100,
    /// <summary>InvalidBatch</summary>
    InvalidBatch = 101,
    /// <summary>InvalidBatchData</summary>
    InvalidBatchData = 102,
    /// <summary>DirectDebitNotSupported</summary>
    DirectDebitNotSupported = 103,
    /// <summary>InvalidSubscriptionId</summary>
    InvalidSubscriptionId = 104,
    /// <summary>NoDataToUpdateThePaymentMethod</summary>
    NoDataToUpdateThePaymentMethod = 105,
    /// <summary>PaymentMethodMustBeOfTypeCard</summary>
    PaymentMethodMustBeOfTypeCard = 106,
    /// <summary>UnableToUpdateThePaymentMethod</summary>
    UnableToUpdateThePaymentMethod = 107,
    /// <summary>UnableToUpdateSubscription</summary>
    UnableToUpdateSubscription = 108,
    /// <summary>UnableToCreateWebhookSubscription</summary>
    UnableToCreateWebhookSubscription = 109,
    /// <summary>UnableToGetWebhookSubscription</summary>
    UnableToGetWebhookSubscription = 110,
    /// <summary>UnableToDeleteWebhookSubscription</summary>
    UnableToDeleteWebhookSubscription = 111,
    /// <summary>BlockedByFraudRules</summary>
    BlockedByFraudRules = 112,
    /// <summary>Require3DSecure</summary>
    Require3DSecure = 113,
    /// <summary>UnableToCreateNetworkToken</summary>
    UnableToCreateNetworkToken = 114,
    /// <summary>UnableToCreatePaymentLink</summary>
    UnableToCreatePaymentLink = 115,
    /// <summary>InvalidPaymentLinkId</summary>
    InvalidPaymentLinkId = 116,
    /// <summary>InvalidFallbackProviderId</summary>
    InvalidFallbackProviderId = 117,
    /// <summary>InvalidDisputeId</summary>
    InvalidDisputeId = 118,
    /// <summary>ApplePayUnknownPublicKeyHash</summary>
    ApplePayUnknownPublicKeyHash = 119,
    /// <summary>ApplePayPlatformModeUnsupported</summary>
    ApplePayPlatformModeUnsupported = 120,
    /// <summary>ApplePayTokenVerificationFailed</summary>
    ApplePayTokenVerificationFailed = 121,
    /// <summary>ApplePayTokenDecryptionFailed</summary>
    ApplePayTokenDecryptionFailed = 122,
    /// <summary>StopRecurringPayments</summary>
    StopRecurringPayments = 123,
    /// <summary>ReferToCardIssuer</summary>
    ReferToCardIssuer = 901,
    /// <summary>ReferToCardIssuerSpecialConditions</summary>
    ReferToCardIssuerSpecialConditions = 902,
    /// <summary>InvalidMerchant</summary>
    InvalidMerchant = 903,
    /// <summary>PickUpCard</summary>
    PickUpCard = 904,
    /// <summary>DoNotHonor</summary>
    DoNotHonor = 905,
    /// <summary>Error</summary>
    Error = 906,
    /// <summary>PickUpCardSpecialCondition</summary>
    PickUpCardSpecialCondition = 907,
    /// <summary>HonourWithIdentification</summary>
    HonourWithIdentification = 908,
    /// <summary>RequestInProgress</summary>
    RequestInProgress = 909,
    /// <summary>ApprovedForPartialAmount</summary>
    ApprovedForPartialAmount = 910,
    /// <summary>ApprovedVip</summary>
    ApprovedVip = 911,
    /// <summary>InvalidTransaction</summary>
    InvalidTransaction = 912,
    /// <summary>IssuerInvalidAmount</summary>
    IssuerInvalidAmount = 913,
    /// <summary>InvalidCardNumberNoSuchNumber</summary>
    InvalidCardNumberNoSuchNumber = 914,
    /// <summary>NoSuchIssuer</summary>
    NoSuchIssuer = 915,
    /// <summary>ApprovedUpdateTrack3</summary>
    ApprovedUpdateTrack3 = 916,
    /// <summary>CustomerCancellation</summary>
    CustomerCancellation = 917,
    /// <summary>CustomerDispute</summary>
    CustomerDispute = 918,
    /// <summary>ReEnterTransaction</summary>
    ReEnterTransaction = 919,
    /// <summary>InvalidResponse</summary>
    InvalidResponse = 920,
    /// <summary>NoActionTaken</summary>
    NoActionTaken = 921,
    /// <summary>SuspectedMalfunction</summary>
    SuspectedMalfunction = 922,
    /// <summary>UnacceptableTransactionFee</summary>
    UnacceptableTransactionFee = 923,
    /// <summary>FileUpdateNotSupportedByReceiver</summary>
    FileUpdateNotSupportedByReceiver = 924,
    /// <summary>UnableToLocateRecordOnFile</summary>
    UnableToLocateRecordOnFile = 925,
    /// <summary>DuplicateFileUpdateRecordOldRecordReplaced</summary>
    DuplicateFileUpdateRecordOldRecordReplaced = 926,
    /// <summary>FileUpdateFieldEditError</summary>
    FileUpdateFieldEditError = 927,
    /// <summary>FileUpdateFileLockedOut</summary>
    FileUpdateFileLockedOut = 928,
    /// <summary>FileUpdateNotSuccessfulContactAcquirer</summary>
    FileUpdateNotSuccessfulContactAcquirer = 929,
    /// <summary>FormatError</summary>
    FormatError = 930,
    /// <summary>BankNotSupportedBySwitch</summary>
    BankNotSupportedBySwitch = 931,
    /// <summary>CompletedPartially</summary>
    CompletedPartially = 932,
    /// <summary>ExpiredCardPickUp</summary>
    ExpiredCardPickUp = 933,
    /// <summary>SuspectedFraudPickUp</summary>
    SuspectedFraudPickUp = 934,
    /// <summary>CardAcceptorContactAcquirerPickUp</summary>
    CardAcceptorContactAcquirerPickUp = 935,
    /// <summary>RestrictedCardPickUp</summary>
    RestrictedCardPickUp = 936,
    /// <summary>CardAcceptorCallAcquirerSecurityPickUp</summary>
    CardAcceptorCallAcquirerSecurityPickUp = 937,
    /// <summary>AllowablePinTriesExceeded</summary>
    AllowablePinTriesExceeded = 938,
    /// <summary>NoCreditAccount</summary>
    NoCreditAccount = 939,
    /// <summary>RequestedFunctionNotSupported</summary>
    RequestedFunctionNotSupported = 940,
    /// <summary>LostCardPickUp</summary>
    LostCardPickUp = 941,
    /// <summary>NoUniversalAccount</summary>
    NoUniversalAccount = 942,
    /// <summary>StolenCardPickUp</summary>
    StolenCardPickUp = 943,
    /// <summary>NoInvestmentAccount</summary>
    NoInvestmentAccount = 944,
    /// <summary>IssuerInsufficientFunds</summary>
    IssuerInsufficientFunds = 951,
    /// <summary>NoCheckingAccount</summary>
    NoCheckingAccount = 952,
    /// <summary>NoSavingsAccount</summary>
    NoSavingsAccount = 953,
    /// <summary>IssuerExpiredCard</summary>
    IssuerExpiredCard = 954,
    /// <summary>IncorrectPersonalIdentificationNumber</summary>
    IncorrectPersonalIdentificationNumber = 955,
    /// <summary>NoCardRecord</summary>
    NoCardRecord = 956,
    /// <summary>TransactionNotPermittedToCardholder</summary>
    TransactionNotPermittedToCardholder = 957,
    /// <summary>TransactionNotPermittedToTerminal</summary>
    TransactionNotPermittedToTerminal = 958,
    /// <summary>SuspectedFraud</summary>
    SuspectedFraud = 959,
    /// <summary>CardAcceptorContactAcquirer</summary>
    CardAcceptorContactAcquirer = 960,
    /// <summary>ExceedsWithdrawalAmountLimit</summary>
    ExceedsWithdrawalAmountLimit = 961,
    /// <summary>RestrictedCard</summary>
    RestrictedCard = 962,
    /// <summary>SecurityViolation</summary>
    SecurityViolation = 963,
    /// <summary>OriginalAmountIncorrect</summary>
    OriginalAmountIncorrect = 964,
    /// <summary>ExceedsWithdrawalFrequencyLimit</summary>
    ExceedsWithdrawalFrequencyLimit = 965,
    /// <summary>CardAcceptorCallAcquirersSecurityDepartment</summary>
    CardAcceptorCallAcquirersSecurityDepartment = 966,
    /// <summary>HardCaptureRequiresCardPickUpAtAtm</summary>
    HardCaptureRequiresCardPickUpAtAtm = 967,
    /// <summary>ResponseReceivedTooLate</summary>
    ResponseReceivedTooLate = 968,
    /// <summary>AllowableNumberOfPinTriesExceeded</summary>
    AllowableNumberOfPinTriesExceeded = 975,
    /// <summary>CutoffIsInProcess</summary>
    CutoffIsInProcess = 990,
    /// <summary>IssuerOrSwitchIsInoperative</summary>
    IssuerOrSwitchIsInoperative = 991,
    /// <summary>FinancialInstitutionOrIntermediateNetworkFacilityCannotBeFoundForRouting</summary>
    FinancialInstitutionOrIntermediateNetworkFacilityCannotBeFoundForRouting = 992,
    /// <summary>TransactionCannotBeCompletedViolationOfLaw</summary>
    TransactionCannotBeCompletedViolationOfLaw = 993,
    /// <summary>DuplicateTransmission</summary>
    DuplicateTransmission = 994,
    /// <summary>ReconcileError</summary>
    ReconcileError = 995,
    /// <summary>SystemMalfunction</summary>
    SystemMalfunction = 996
}
