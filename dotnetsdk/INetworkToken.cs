using System.Threading.Tasks;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface INetworkToken
    {
        NetworkTokenData TokeniseTokenConnect(NewTokenConnect tokenConnect);
        Task<NetworkTokenData> TokeniseTokenConnectAsync(NewTokenConnect tokenConnect);
        NetworkTokenData CreateFromPaymentMethod(NewTokenisePaymentMethod paymentMethod);
        Task<NetworkTokenData> CreateFromPaymentMethodAsync(NewTokenisePaymentMethod paymentMethod);
    }
}
