using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models.Dispute;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class Dispute : IDispute
    {
        public DisputeList Search(DisputeSearch search)
        {
            var queryString = BuildSearchQueryString(search);

            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, DisputeList>("/dispute" + queryString, Method.GET, null));
        }

        public async Task<DisputeList> SearchAsync(DisputeSearch search)
        {
            var queryString = BuildSearchQueryString(search);

            return await HttpWrapper.CallAsync<string, DisputeList>("/dispute" + queryString, Method.GET, null);
        }

        public DisputeData Single(string disputeId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, DisputeData>("/dispute/" + disputeId, Method.GET, null));
        }

        public async Task<DisputeData> SingleAsync(string disputeId)
        {
            return await HttpWrapper.CallAsync<string, DisputeData>("/dispute/" + disputeId, Method.GET, null);
        }

        private static string BuildSearchQueryString(DisputeSearch search)
        {
            var queryString = new List<string>();

            if (search.Skip.HasValue)
                queryString.Add("skip=" + search.Skip.Value);

            if (search.Limit.HasValue)
                queryString.Add("limit=" + search.Limit.Value);

            if (!string.IsNullOrWhiteSpace(search.TransactionId))
                queryString.Add("transactionId=" + HttpUtility.UrlEncode(search.TransactionId));

            if (!string.IsNullOrWhiteSpace(search.ProviderId))
                queryString.Add("providerId=" + HttpUtility.UrlEncode(search.ProviderId));

            if (!string.IsNullOrWhiteSpace(search.Status))
                queryString.Add("status=" + HttpUtility.UrlEncode(search.Status));

            if (!string.IsNullOrWhiteSpace(search.Stage))
                queryString.Add("stage=" + HttpUtility.UrlEncode(search.Stage));

            if (!string.IsNullOrWhiteSpace(search.ReasonCategory))
                queryString.Add("reasonCategory=" + HttpUtility.UrlEncode(search.ReasonCategory));

            if (!string.IsNullOrWhiteSpace(search.Currency))
                queryString.Add("currency=" + HttpUtility.UrlEncode(search.Currency));

            if (search.AmountGreaterThan.HasValue)
                queryString.Add("amountGreaterThan=" + search.AmountGreaterThan.Value);

            if (search.AmountLessThan.HasValue)
                queryString.Add("amountLessThan=" + search.AmountLessThan.Value);

            if (search.AddedAfter.HasValue)
                queryString.Add("addedAfter=" +
                                HttpUtility.UrlEncode(search.AddedAfter.Value.ToString("yyyy-MM-dd HH:mm:ss")));

            if (search.AddedBefore.HasValue)
                queryString.Add("addedBefore=" +
                                HttpUtility.UrlEncode(search.AddedBefore.Value.ToString("yyyy-MM-dd HH:mm:ss")));

            if (search.DueBefore.HasValue)
                queryString.Add("dueBefore=" +
                                HttpUtility.UrlEncode(search.DueBefore.Value.ToString("yyyy-MM-dd HH:mm:ss")));

            if (!string.IsNullOrWhiteSpace(search.SortBy))
                queryString.Add("sortBy=" + HttpUtility.UrlEncode(search.SortBy));

            if (!string.IsNullOrWhiteSpace(search.SortOrder))
                queryString.Add("sortOrder=" + HttpUtility.UrlEncode(search.SortOrder));

            var result = "";
            if (queryString.Count > 0)
                result = "?" + string.Join("&", queryString);

            return result;
        }
    }
}
