using System.Threading.Tasks;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class Fraud : IFraud
    {
        public FraudCheckStatus Check(FraudCheckData data)
        {
            return AsyncHelper.RunSync(() => CheckAsync(data));
        }

        public async Task<FraudCheckStatus> CheckAsync(FraudCheckData data)
        {
            return await HttpWrapper.CallAsync<FraudCheckData, FraudCheckStatus>(
                "/fraud_check", Method.POST, data);
        }
    }
}
