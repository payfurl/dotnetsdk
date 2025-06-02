using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Models.Token;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{

    public class Token : IToken
    {
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

        public TokenData TokenisePaymentMethod(NewTokenPaymentMethod newTokenPaymentMethod)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewTokenPaymentMethod, TokenData>("/token/payment_method", Method.POST, newTokenPaymentMethod));
        }
        
        public async Task<TokenData> TokenisePaymentMethodAsync(NewTokenPaymentMethod newTokenPaymentMethod)
        {
            return await HttpWrapper.CallAsync<NewTokenPaymentMethod, TokenData>("/token/payment_method", Method.POST, newTokenPaymentMethod);
        }
        
        public TokenData TokeniseCard(NewTokenCard newTokenCard)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewTokenCard, TokenData>("/token/card", Method.POST, newTokenCard, new Dictionary<string, string> 
                {
                    { "ProviderId", newTokenCard.ProviderId }
                }));
        }
        
        public async Task<TokenData> TokeniseCardAsync(NewTokenCard newTokenCard)
        {
            return await HttpWrapper.CallAsync<NewTokenCard, TokenData>("/token/card", Method.POST, newTokenCard, new Dictionary<string, string>
            {
                { "ProviderId", newTokenCard.ProviderId }
            });
        }
        
        public TokenData TokeniseCardLeastCost(NewTokenCard newTokenCardLeastCost)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewTokenCard, TokenData>("/token/card/least_cost", Method.POST, newTokenCardLeastCost));
        }
        
        public async Task<TokenData> TokeniseCardLeastCostAsync(NewTokenCard newTokenCardLeastCost)
        {
            return await HttpWrapper.CallAsync<NewTokenCard, TokenData>("/token/card/least_cost", Method.POST, newTokenCardLeastCost);
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