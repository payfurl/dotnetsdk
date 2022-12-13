using payfurl.sdk;
using payfurl.sdk.Models;
using System.Threading.Tasks;
using Xunit;
using Environment = payfurl.sdk.Environment;

namespace FunctionalTests
{
    public class Vault
    {

        private static readonly NewVault NewVault = new()
        {
            CardNumber = "4111111111111111",
            Ccv = "123"
        };

        public Vault()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Fact]
        public void Create()
        {
            Assert.NotNull(CreateVault().VaultId);
        }

        [Fact]
        public async Task CreateAsync()
        {
            var result = await CreateVaultAsync();
            Assert.NotNull(result.VaultId);
        }

        [Fact]
        public void Single()
        {
            var vault = CreateVault();
            var svc = new payfurl.sdk.Vault();
            var result = svc.Single(vault.VaultId);

            Assert.Equal(result.VaultId, vault.VaultId);
        }

        [Fact]
        public async Task SingleAsync()
        {
            var vault = await CreateVaultAsync();
            var svc = new payfurl.sdk.Vault();
            var result = await svc.SingleAsync(vault.VaultId);

            Assert.Equal(result.VaultId, vault.VaultId);
        }

        [Fact] public void Delete()
        {
            var vault = CreateVault();
            var svc = new payfurl.sdk.Vault();
            var result = svc.Delete(vault.VaultId);

            Assert.Equal(result.VaultId, vault.VaultId);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            var vault = await CreateVaultAsync();
            var svc = new payfurl.sdk.Vault();
            var result = await svc.DeleteAsync(vault.VaultId);

            Assert.Equal(result.VaultId, vault.VaultId);
        }

        private VaultData CreateVault()
        {
            var svc = new payfurl.sdk.Vault();
            return svc.Create(NewVault);
        }

        private async Task<VaultData> CreateVaultAsync()
        {
            var svc = new payfurl.sdk.Vault();
            return await svc.CreateAsync(NewVault);
        }
    }
}
