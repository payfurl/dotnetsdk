using System.Threading.Tasks;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface IThreeDs
    {
        ThreeDsInitiateResponse Initiate(ThreeDsInitiate data);
        Task<ThreeDsInitiateResponse> InitiateAsync(ThreeDsInitiate data);
        ThreeDsAuthenticateResponse Authenticate(ThreeDsAuthenticate data);
        Task<ThreeDsAuthenticateResponse> AuthenticateAsync(ThreeDsAuthenticate data);
        ThreeDsCompleteResponse Complete(ThreeDsComplete data);
        Task<ThreeDsCompleteResponse> CompleteAsync(ThreeDsComplete data);
        ThreeDsInitiateResponse InitiateWithToken(string tokenId, ThreeDsTokenInitiate data);
        Task<ThreeDsInitiateResponse> InitiateWithTokenAsync(string tokenId, ThreeDsTokenInitiate data);
        ThreeDsAuthenticateResponse AuthenticateWithToken(string tokenId, ThreeDsAuthenticate data);
        Task<ThreeDsAuthenticateResponse> AuthenticateWithTokenAsync(string tokenId, ThreeDsAuthenticate data);
    }
}
