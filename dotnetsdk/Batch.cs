using System.Threading.Tasks;
using System.Web;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models.Batch;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class Batch : IBatch
    {
        public BatchStatus CreateTransactionWithPaymentMethod(NewTransactionPaymentMethod data)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewTransactionPaymentMethod, BatchStatus>("/batch/transaction/payment_method", Method.POST,
                    data));
        }

        public async Task<BatchStatus> CreateTransactionWithPaymentMethodAsync(NewTransactionPaymentMethod data)
        {
            return await HttpWrapper.CallAsync<NewTransactionPaymentMethod, BatchStatus>("/batch/transaction/payment_method",
                Method.POST, data);
        }

        public BatchData GetBatch(string batchId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, BatchData>("/batch/" + batchId, Method.GET, null));
        }

        public async Task<BatchData> GetBatchAsync(string batchId)
        {
            return await HttpWrapper.CallAsync<string, BatchData>("/batch/" + batchId, Method.GET, null);
        }

        public BatchStatus GetBatchStatus(string batchId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, BatchStatus>("/batch/" + batchId + "/status", Method.GET, null));
        }

        public async Task<BatchStatus> GetBatchStatusAsync(string batchId)
        {
            return await HttpWrapper.CallAsync<string, BatchStatus>("/batch/" + batchId + "/status", Method.GET, null);
        }

        public BatchList SearchBatch(BatchSearch search)
        {
            var queryString = BuildSearchQueryString(search);

            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, BatchList>("/batch" + queryString, Method.GET, null));
        }

        public async Task<BatchList> SearchBatchAsync(BatchSearch search)
        {
            var queryString = BuildSearchQueryString(search);

            return await HttpWrapper.CallAsync<string, BatchList>("/batch" + queryString, Method.GET, null);
        }

        private static string BuildSearchQueryString(BatchSearch searchData)
        {
            var queryString = "";

            if (searchData.Skip.HasValue)
                queryString = "skip=" + searchData.Skip.Value;

            if (searchData.Limit.HasValue)
                queryString = "limit=" + searchData.Limit.Value;

            if (!string.IsNullOrWhiteSpace(searchData.Description))
                queryString = "description=" + HttpUtility.UrlEncode(searchData.Description);

            if (searchData.AddedAfter.HasValue)
                queryString = "addedAfter=" +
                              HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (searchData.AddedBefore.HasValue)
                queryString = "addedBefore=" +
                              HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return queryString;
        }
    }
}