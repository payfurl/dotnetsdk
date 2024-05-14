using System.Threading.Tasks;
using payfurl.sdk.Models.Batch;

namespace payfurl.sdk
{
    public interface IBatch
    {
        BatchStatus CreateTransactionWithPaymentMethod(NewTransactionPaymentMethod data);
        Task<BatchStatus> CreateTransactionWithPaymentMethodAsync(NewTransactionPaymentMethod data);
        BatchData GetBatch(string batchId);
        Task<BatchData> GetBatchAsync(string batchId);
        BatchStatus GetBatchStatus(string batchId);
        Task<BatchStatus> GetBatchStatusAsync(string batchId);
        BatchList SearchBatch(BatchSearch search);
        Task<BatchList> SearchBatchAsync(BatchSearch search);
    }
}