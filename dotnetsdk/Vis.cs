using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class Vis : IVis
    {
        public JToken GetPlans(GetVisPlansData data)
        {
            return AsyncHelper.RunSync(() => GetPlansAsync(data));
        }

        public async Task<JToken> GetPlansAsync(GetVisPlansData data)
        {
            return await HttpWrapper.CallAsync<GetVisPlansData, JToken>(
                "/vis/plans", Method.POST, data);
        }
    }
}
