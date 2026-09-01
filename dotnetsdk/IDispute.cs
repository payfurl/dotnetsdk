using System.Threading.Tasks;
using payfurl.sdk.Models.Dispute;

namespace payfurl.sdk
{
    public interface IDispute
    {
        DisputeList Search(DisputeSearch search);
        Task<DisputeList> SearchAsync(DisputeSearch search);
        DisputeData Single(string disputeId);
        Task<DisputeData> SingleAsync(string disputeId);
    }
}
