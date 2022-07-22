using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface IVault
    {
        VaultData Create(NewVault newVault);
        VaultData Delete(string vaultId);
        VaultDataWithPCI Single(string vaultId);
    }
}