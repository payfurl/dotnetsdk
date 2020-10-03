using payfurl.sdk.Models;
using payfurl.sdk.Tools;
using System.Web;

namespace payfurl.sdk
{
    public class Transfer : ITransfer
    {
        public TransferData Create(NewTransfer newTransfer)
        {
            return HttpWrapper.Call<NewTransfer, TransferData>("/transfer", Method.POST, newTransfer);
        }

        public TransferData Single(string transferId)
        {
            return HttpWrapper.Call<string, TransferData>("/transfer/" + transferId, Method.GET, null);
        }

        public TransferList Search(TransferSearch searchData)
        {
            // TODO: move into a shared class to handle formatting
            var queryString = "";

            if (searchData.Skip.HasValue)
                queryString = "Skip=" + searchData.Skip.Value;

            if (searchData.Limit.HasValue)
                queryString = "Limit=" + searchData.Limit.Value;

            if (!string.IsNullOrWhiteSpace(searchData.Reference))
                queryString = "Reference=" + HttpUtility.UrlEncode(searchData.Reference);

            if (!string.IsNullOrWhiteSpace(searchData.ProviderId))
                queryString = "ProviderId=" + HttpUtility.UrlEncode(searchData.ProviderId);

            if (!string.IsNullOrWhiteSpace(searchData.Status))
                queryString = "Status=" + HttpUtility.UrlEncode(searchData.Status);

            if (searchData.AddedAfter.HasValue)
                queryString = "AddedAfter=" + HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (searchData.AddedBefore.HasValue)
                queryString = "AddedBefore=" + HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (!string.IsNullOrWhiteSpace(searchData.SortBy))
                queryString = "SortBy=" + searchData.SortBy;

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return HttpWrapper.Call<string, TransferList>("/transfer" + queryString, Method.GET, null);
        }
    }
}
