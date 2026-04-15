using System.Threading.Tasks;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface IVersion
    {
        ApplicationVersion Get();
        Task<ApplicationVersion> GetAsync();
    }
}
