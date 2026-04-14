using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface IVis
    {
        JToken GetPlans(GetVisPlansData data);
        Task<JToken> GetPlansAsync(GetVisPlansData data);
    }
}
