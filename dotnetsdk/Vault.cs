using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class Vault : IVault
    {
        public VaultData Create(NewVault newVault)
        {
            return HttpWrapper.Call<NewVault, VaultData>("/vault", Method.POST, newVault);
        }

        public VaultData Delete(string vaultId)
        {
            return HttpWrapper.Call<string, VaultData>("/vault/" + vaultId, Method.DELETE, null);
        }

        public VaultDataWithPCI Single(string vaultId)
        {
            return HttpWrapper.Call<string, VaultDataWithPCI>("/vault/" + vaultId, Method.GET, null);
        }
    }
}