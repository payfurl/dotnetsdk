using System.Threading.Tasks;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface IProvider
    {
        Models.Provider Create(NewProvider newProvider);
        Task<Models.Provider> CreateAsync(NewProvider newProvider);
        Models.Provider Update(string providerId, UpdateProvider updateProvider);
        Task<Models.Provider> UpdateAsync(string providerId, UpdateProvider updateProvider);
    }
}
