using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class Token : IToken
    {
        public PaymentToken TokenisePaymentMethod(NewTokenPaymentMethod paymentMethodToken)
        {
            return AsyncHelper.RunSync(() => TokenisePaymentMethodAsync(paymentMethodToken));
        }

        public async Task<PaymentToken> TokenisePaymentMethodAsync(NewTokenPaymentMethod paymentMethodToken)
        {
            return await HttpWrapper.CallAsync<NewTokenPaymentMethod, PaymentToken>(
                "/token/payment_method", Method.POST, paymentMethodToken);
        }

        public PaymentToken TokeniseCard(NewTokenCard cardToken, string providerId)
        {
            return AsyncHelper.RunSync(() => TokeniseCardAsync(cardToken, providerId));
        }

        public async Task<PaymentToken> TokeniseCardAsync(NewTokenCard cardToken, string providerId)
        {
            var headers = new Dictionary<string, string>
            {
                { "providerId", providerId }
            };
            return await HttpWrapper.CallAsync<NewTokenCard, PaymentToken>(
                "/token/card", Method.POST, cardToken, headers);
        }

        public PaymentToken TokeniseCardLeastCost(NewTokenCard cardToken)
        {
            return AsyncHelper.RunSync(() => TokeniseCardLeastCostAsync(cardToken));
        }

        public async Task<PaymentToken> TokeniseCardLeastCostAsync(NewTokenCard cardToken)
        {
            return await HttpWrapper.CallAsync<NewTokenCard, PaymentToken>(
                "/token/card/least_cost", Method.POST, cardToken);
        }

        public EncryptedTokenCardData GetTokenisedCard(string tokenId, string providerId)
        {
            return AsyncHelper.RunSync(() => GetTokenisedCardAsync(tokenId, providerId));
        }

        public async Task<EncryptedTokenCardData> GetTokenisedCardAsync(string tokenId, string providerId)
        {
            var headers = new Dictionary<string, string>
            {
                { "providerId", providerId }
            };
            return await HttpWrapper.CallAsync<string, EncryptedTokenCardData>(
                "/token/" + tokenId + "/card", Method.GET, null, headers);
        }

        public void UpdateExternalThreeDsData(string tokenId, Dictionary<string, string> externalThreeDsData)
        {
            AsyncHelper.RunSync(() => UpdateExternalThreeDsDataAsync(tokenId, externalThreeDsData));
        }

        public async Task UpdateExternalThreeDsDataAsync(string tokenId, Dictionary<string, string> externalThreeDsData)
        {
            await HttpWrapper.CallAsync<Dictionary<string, string>, string>(
                "/token/" + tokenId + "/external3dsdata", Method.PUT, externalThreeDsData);
        }

        public PaymentToken TokenisePayTo(NewPayToAgreement payToToken)
        {
            return AsyncHelper.RunSync(() => TokenisePayToAsync(payToToken));
        }

        public async Task<PaymentToken> TokenisePayToAsync(NewPayToAgreement payToToken)
        {
            return await HttpWrapper.CallAsync<NewPayToAgreement, PaymentToken>(
                "/token/payto", Method.POST, payToToken);
        }

        public TokenData Single(string tokenId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, TokenData>("/token/" + tokenId, Method.GET, null));
        }

        public async Task<TokenData> SingleAsync(string tokenId)
        {
            return await HttpWrapper.CallAsync<string, TokenData>("/token/" + tokenId, Method.GET, null);
        }

        public TokenDataList Search(TokenSearch searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, TokenDataList>("/token" + queryString, Method.GET, null));
        }

        public async Task<TokenDataList> SearchAsync(TokenSearch searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return await HttpWrapper.CallAsync<string, TokenDataList>("/token" + queryString, Method.GET, null);
        }

        public void UpdateAuthenticationData(string tokenId, Dictionary<string, string> authenticationData)
        {
            AsyncHelper.RunSync(() => UpdateAuthenticationDataAsync(tokenId, authenticationData));
        }

        public async Task UpdateAuthenticationDataAsync(string tokenId, Dictionary<string, string> authenticationData)
        {
            await HttpWrapper.CallAsync<Dictionary<string, string>, string>(
                "/token/" + tokenId + "/authentication", Method.PUT, authenticationData);
        }

        private static string BuildSearchQueryString(TokenSearch searchData)
        {
            // TODO: move into a shared class to handle formatting
            var queryString = new List<string>();

            if (!string.IsNullOrWhiteSpace(searchData.ProviderId))
                queryString.Add("ProviderId=" + HttpUtility.UrlEncode(searchData.ProviderId));

            if (searchData.AddedAfter.HasValue)
                queryString.Add("AddedAfter=" +
                                HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss")));

            if (searchData.AddedBefore.HasValue)
                queryString.Add("AddedBefore=" +
                                HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss")));

            if (!string.IsNullOrWhiteSpace(searchData.SortBy))
                queryString.Add("SortBy=" + searchData.SortBy);

            if (!string.IsNullOrWhiteSpace(searchData.Status))
                queryString.Add("Status=" + HttpUtility.UrlEncode(searchData.Status));

            if (searchData.Skip.HasValue)
                queryString.Add("Skip=" + searchData.Skip.Value);

            if (searchData.Limit.HasValue)
                queryString.Add("Limit=" + searchData.Limit.Value);

            var result = "";
            if (queryString.Count > 0)
                result = "?" + string.Join("&", queryString);
            
            return result;
        }
    }
}
