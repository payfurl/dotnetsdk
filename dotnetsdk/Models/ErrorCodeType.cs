﻿using System.ComponentModel;

namespace payfurl.sdk.Models
{
    public enum ErrorCodeType
    {
        NoError = 0,
        UnknownError = 1,
        InvalidAmount = 2,
        InvalidAuthoriseDetails = 3,
        InvalidCaptureDetails = 4,
        InvalidCardNumber = 5,
        InvalidCcv = 6,
        InvalidCountry = 7,
        InvalidCountryForTransfer = 8,
        InvalidExpiryDate = 9,
        InvalidPaymentDetails = 10,
        InvalidPaypalAccount = 11,
        InvalidRefundDetails = 12,
        InvalidVoidDetails = 13,
        ExpiredCard = 14,
        InsufficientFunds = 15,
        LocationNotSupported = 16,
        PaypalBillingAgreementCancelled = 17,
        ThreeDSecureFailed = 18,
        AccountEmailUnconfirmed = 19,
        AccountError = 20,
        AccountIsLockedOrClosed = 21,
        AccountNotAuthorised = 22,
        AccountRestricted = 23,
        AmountExceedsMaxAmountForPayToAgreement = 24,
        AmountTooLarge = 25,
        AuthenticationAuthorizationError = 26,
        BillingPostalCodeNotSupported = 27,
        CurrencyComplianceIssuesWithTransactions = 28,
        CurrencyNotSupported = 29,
        Duplicate = 30,
        DuplicateTransaction = 31,
        FeatureUnavailable = 32,
        PayToAgreementIsNotActive = 33,
        ProviderServiceError = 34,
        RateLimitingApplied = 35,
        ReceiverAccountLocked = 36,
        RetryLater = 37,
        SenderAccountIssue = 38,
        InvalidProviderId = 39,
        InvalidAuthentication = 40,
        InvalidEnvironment = 41,
        TokeniseCardError = 42,
        TransactionError = 43,
        NotSupportCharge = 44,
        UnableToRefund = 45,
        UnableToCharge = 46,
        UnableToCapture = 47,
        UnableToVoid = 48,
        UnableToAddCustomer = 49,
        NoTransactionFound = 50,
        InvalidOrExpiredToken = 51,
        InvalidCustomerId = 52,
        InvalidCharge = 53,
        InvalidChargeStatus = 54,
        InvalidCardLength = 55,
        InvalidPaymentMethodId = 56,
        InvalidPaymentMethod = 57,
        InvalidPaymentType = 58,
        InvalidThreeDSServerTransID = 59,
        InvalidTransferAmount = 60,
        InvalidReferenceId = 61,
        InvalidVaultId = 62,
        NoCardProvidersConfigured = 63,
        CustomerHasNoDefaultPaymentMethodId = 64,
        NoProviderFound = 65,
        NoVaultIdAssociatedWithThisPaymentMethod = 66,
        NoTransferInformationProvided = 67,
        TransferLargerThanOriginalCharge = 68,
        DuplicateGroupReference = 69,
        DuplicateTransferReference = 70,
        FeatureAuthoriseAndCaptureIsNotSupported = 71,
        InvalidCustomerIdRemoved = 72,
        InvalidPaymentMethodIdRemoved = 73,
        InvalidAmountMustBePositive = 74,
        FeatureVerifyPendingIsNotSupported = 75,
        InvalidAmountMaximumAvailableRefund = 76,
        InvalidCaptureAmountPassed = 77,
        FeaturePartialCaptureIsNotSupportedCaptureAmount = 78,
        AmountOutsideRangeForMerchant = 79,
        GatewayTimout = 80,
        InvalidPaymentInformation = 81,
        CannotProvideBothExpireDateAndExpireSeconds = 82,
        InvalidExpireDateOrExpireSeconds = 83,
        InvalidChargeId = 84,
        CannotProvideBothVaultExpireDateAndVaultExpireSeconds = 85,
        InvalidVaultExpiryDate = 86,
        InvalidTokenId = 87,
        InvalidTransferId = 88,
        InvalidNotificationFormat = 89,
        ValidationError = 90,
        TokenVerificationNotSupported = 91,
        InvalidAddress = 92,
        CardBlocked = 93,
        Timeout = 94
    }
}