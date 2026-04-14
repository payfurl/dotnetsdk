using System.Threading.Tasks;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class NetworkToken : INetworkToken
    {
        public NetworkTokenData TokeniseTokenConnect(NewTokenConnect tokenConnect)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewTokenConnect, NetworkTokenData>("/token_connect", Method.POST, tokenConnect));
        }

        public async Task<NetworkTokenData> TokeniseTokenConnectAsync(NewTokenConnect tokenConnect)
        {
            return await HttpWrapper.CallAsync<NewTokenConnect, NetworkTokenData>("/token_connect", Method.POST, tokenConnect);
        }

        public NetworkTokenData CreateFromPaymentMethod(NewTokenisePaymentMethod paymentMethod)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewTokenisePaymentMethod, NetworkTokenData>(
                    "/network_token/payment_method", Method.POST, paymentMethod));
        }

        public async Task<NetworkTokenData> CreateFromPaymentMethodAsync(NewTokenisePaymentMethod paymentMethod)
        {
            return await HttpWrapper.CallAsync<NewTokenisePaymentMethod, NetworkTokenData>(
                "/network_token/payment_method", Method.POST, paymentMethod);
        }
    }
}
