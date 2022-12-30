using System.Threading.Tasks;
using System.Web;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
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

        private static string BuildSearchQueryString(TokenSearch searchData)
        {
            // TODO: move into a shared class to handle formatting
            var queryString = "";

            if (!string.IsNullOrWhiteSpace(searchData.ProviderId))
                queryString = "ProviderId=" + HttpUtility.UrlEncode(searchData.ProviderId);

            if (searchData.AddedAfter.HasValue)
                queryString = "AddedAfter=" +
                              HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (searchData.AddedBefore.HasValue)
                queryString = "AddedBefore=" +
                              HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (!string.IsNullOrWhiteSpace(searchData.SortBy))
                queryString = "SortBy=" + searchData.SortBy;

            if (!string.IsNullOrWhiteSpace(searchData.Status))
                queryString = "Status=" + HttpUtility.UrlEncode(searchData.Status);

            if (searchData.Skip.HasValue)
                queryString = "Skip=" + searchData.Skip.Value;

            if (searchData.Limit.HasValue)
                queryString = "Limit=" + searchData.Limit.Value;

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return queryString;
        }
    }
}