using payfurl.sdk.Models;
using payfurl.sdk.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using payfurl.sdk.Helpers;

namespace payfurl.sdk
{
    public class Transfer : ITransfer
    {
        public List<TransferData> Create(NewTransferGroup newTransfer)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewTransferGroup, List<TransferData>>("/transfer", Method.POST, newTransfer));
        }

        public async Task<List<TransferData>> CreateAsync(NewTransferGroup newTransfer)
        {
            return await HttpWrapper.CallAsync<NewTransferGroup, List<TransferData>>("/transfer", Method.POST,
                newTransfer);
        }

        public TransferData Single(string transferId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, TransferData>("/transfer/" + transferId, Method.GET, null));
        }

        public async Task<TransferData> SingleAsync(string transferId)
        {
            return await HttpWrapper.CallAsync<string, TransferData>("/transfer/" + transferId, Method.GET, null);
        }

        public TransferList Search(TransferSearch searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, TransferList>("/transfer" + queryString, Method.GET, null));
        }

        public async Task<TransferList> SearchAsync(TransferSearch searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return await HttpWrapper.CallAsync<string, TransferList>("/transfer" + queryString, Method.GET, null);
        }

        private static string BuildSearchQueryString(TransferSearch searchData)
        {
            // TODO: move into a shared class to handle formatting
            var queryString = new List<string>();

            if (searchData.Skip.HasValue)
                queryString.Add("Skip=" + searchData.Skip.Value);

            if (searchData.Limit.HasValue)
                queryString.Add("Limit=" + searchData.Limit.Value);

            if (!string.IsNullOrWhiteSpace(searchData.Reference))
                queryString.Add("Reference=" + HttpUtility.UrlEncode(searchData.Reference));

            if (!string.IsNullOrWhiteSpace(searchData.ProviderId))
                queryString.Add("ProviderId=" + HttpUtility.UrlEncode(searchData.ProviderId));

            if (!string.IsNullOrWhiteSpace(searchData.Status))
                queryString.Add("Status=" + HttpUtility.UrlEncode(searchData.Status));

            if (searchData.AddedAfter.HasValue)
                queryString.Add("AddedAfter=" +
                                HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss")));

            if (searchData.AddedBefore.HasValue)
                queryString.Add("AddedBefore=" +
                                HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss")));

            if (!string.IsNullOrWhiteSpace(searchData.SortBy))
                queryString.Add("SortBy=" + searchData.SortBy);

            var result = "";
            if (queryString.Count > 0)
                result = "?" + string.Join("&", queryString);
            
            return result;
        }
    }
}