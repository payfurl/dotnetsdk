using System.Threading.Tasks;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class Version : IVersion
    {
        public ApplicationVersion Get()
        {
            return AsyncHelper.RunSync(GetAsync);
        }

        public async Task<ApplicationVersion> GetAsync()
        {
            return await HttpWrapper.CallAsync<string, ApplicationVersion>("/version", Method.GET, null);
        }
    }
}
