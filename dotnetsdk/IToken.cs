using System.Collections.Generic;
using payfurl.sdk.Models;
using System.Threading.Tasks;

namespace payfurl.sdk
{
    public interface IToken
    {
        PaymentToken TokenisePaymentMethod(NewTokenPaymentMethod paymentMethodToken);
        Task<PaymentToken> TokenisePaymentMethodAsync(NewTokenPaymentMethod paymentMethodToken);
        PaymentToken TokeniseCard(NewTokenCard cardToken, string providerId);
        Task<PaymentToken> TokeniseCardAsync(NewTokenCard cardToken, string providerId);
        PaymentToken TokeniseCardLeastCost(NewTokenCard cardToken);
        Task<PaymentToken> TokeniseCardLeastCostAsync(NewTokenCard cardToken);
        EncryptedTokenCardData GetTokenisedCard(string tokenId, string providerId);
        Task<EncryptedTokenCardData> GetTokenisedCardAsync(string tokenId, string providerId);
        void UpdateExternalThreeDsData(string tokenId, Dictionary<string, string> externalThreeDsData);
        Task UpdateExternalThreeDsDataAsync(string tokenId, Dictionary<string, string> externalThreeDsData);
        PaymentToken TokenisePayTo(NewPayToAgreement payToToken);
        Task<PaymentToken> TokenisePayToAsync(NewPayToAgreement payToToken);
        TokenData Single(string tokenId);
        Task<TokenData> SingleAsync(string tokenId);
        TokenDataList Search(TokenSearch searchData);
        Task<TokenDataList> SearchAsync(TokenSearch searchData);
        void UpdateAuthenticationData(string tokenId, Dictionary<string, string> authenticationData);
        Task UpdateAuthenticationDataAsync(string tokenId, Dictionary<string, string> authenticationData);
    }
}
