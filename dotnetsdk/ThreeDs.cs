using System.Threading.Tasks;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class ThreeDs : IThreeDs
    {
        public ThreeDsInitiateResponse Initiate(ThreeDsInitiate data)
        {
            return AsyncHelper.RunSync(() => InitiateAsync(data));
        }

        public async Task<ThreeDsInitiateResponse> InitiateAsync(ThreeDsInitiate data)
        {
            return await HttpWrapper.CallAsync<ThreeDsInitiate, ThreeDsInitiateResponse>(
                "/3ds/preauth", Method.POST, data);
        }

        public ThreeDsAuthenticateResponse Authenticate(ThreeDsAuthenticate data)
        {
            return AsyncHelper.RunSync(() => AuthenticateAsync(data));
        }

        public async Task<ThreeDsAuthenticateResponse> AuthenticateAsync(ThreeDsAuthenticate data)
        {
            return await HttpWrapper.CallAsync<ThreeDsAuthenticate, ThreeDsAuthenticateResponse>(
                "/3ds/auth", Method.POST, data);
        }

        public ThreeDsCompleteResponse Complete(ThreeDsComplete data)
        {
            return AsyncHelper.RunSync(() => CompleteAsync(data));
        }

        public async Task<ThreeDsCompleteResponse> CompleteAsync(ThreeDsComplete data)
        {
            return await HttpWrapper.CallAsync<ThreeDsComplete, ThreeDsCompleteResponse>(
                "/3ds/postauth", Method.POST, data);
        }

        public ThreeDsInitiateResponse InitiateWithToken(string tokenId, ThreeDsTokenInitiate data)
        {
            return AsyncHelper.RunSync(() => InitiateWithTokenAsync(tokenId, data));
        }

        public async Task<ThreeDsInitiateResponse> InitiateWithTokenAsync(string tokenId, ThreeDsTokenInitiate data)
        {
            return await HttpWrapper.CallAsync<ThreeDsTokenInitiate, ThreeDsInitiateResponse>(
                "/3ds/preauth/token/" + tokenId, Method.POST, data);
        }

        public ThreeDsAuthenticateResponse AuthenticateWithToken(string tokenId, ThreeDsAuthenticate data)
        {
            return AsyncHelper.RunSync(() => AuthenticateWithTokenAsync(tokenId, data));
        }

        public async Task<ThreeDsAuthenticateResponse> AuthenticateWithTokenAsync(string tokenId,
            ThreeDsAuthenticate data)
        {
            return await HttpWrapper.CallAsync<ThreeDsAuthenticate, ThreeDsAuthenticateResponse>(
                "/3ds/auth/token/" + tokenId, Method.POST, data);
        }
    }
}
