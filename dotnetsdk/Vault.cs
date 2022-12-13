using System.Threading.Tasks;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class Vault : IVault
    {
        public VaultData Create(NewVault newVault)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<NewVault, VaultData>("/vault", Method.POST, newVault));
        }

        public async Task<VaultData> CreateAsync(NewVault newVault)
        {
            return await HttpWrapper.CallAsync<NewVault, VaultData>("/vault", Method.POST, newVault);
        }

        public VaultData Delete(string vaultId)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<string, VaultData>("/vault/" + vaultId, Method.DELETE, null));
        }

        public async Task<VaultData> DeleteAsync(string vaultId)
        {
            return await HttpWrapper.CallAsync<string, VaultData>("/vault/" + vaultId, Method.DELETE, null);
        }

        public VaultDataWithPCI Single(string vaultId)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<string, VaultDataWithPCI>("/vault/" + vaultId, Method.GET, null));
        }

        public async Task<VaultDataWithPCI> SingleAsync(string vaultId)
        {
            return await HttpWrapper.CallAsync<string, VaultDataWithPCI>("/vault/" + vaultId, Method.GET, null);
        }
    }
}