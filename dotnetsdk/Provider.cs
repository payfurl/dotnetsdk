using payfurl.sdk.Tools;
using System.Threading.Tasks;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public class Provider : IProvider
    {
        public Models.Provider Create(NewProvider newProvider)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewProvider, Models.Provider>("/provider", Method.POST, newProvider));
        }

        public async Task<Models.Provider> CreateAsync(NewProvider newProvider)
        {
            return await HttpWrapper.CallAsync<NewProvider, Models.Provider>("/provider", Method.POST,
                newProvider);
        }

        public Models.Provider Update(string providerId, UpdateProvider updateProvider)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<UpdateProvider, Models.Provider>("/provider/" + providerId, Method.PUT, updateProvider));
        }

        public async Task<Models.Provider> UpdateAsync(string providerId, UpdateProvider updateProvider)
        {
            return await HttpWrapper.CallAsync<UpdateProvider, Models.Provider>("/provider/" + providerId, Method.PUT,
                updateProvider);
        }

    }
}
