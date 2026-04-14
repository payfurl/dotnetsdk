using System.Threading.Tasks;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface IFraud
    {
        FraudCheckStatus Check(FraudCheckData data);
        Task<FraudCheckStatus> CheckAsync(FraudCheckData data);
    }
}
