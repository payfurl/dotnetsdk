using System;
using payfurl.sdk;
using payfurl.sdk.Models;
using System.Collections.Generic;
using Xunit;
using Environment = payfurl.sdk.Environment;

namespace FunctionalTests
{
    public class Vault
    {
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
        public void Single()
        {
            var vault = CreateVault();
            var svc = new payfurl.sdk.Vault();
            var result = svc.Single(vault.VaultId);

            Assert.Equal(result.VaultId, vault.VaultId);
        }

        [Fact] public void Delete()
        {
            var vault = CreateVault();
            var svc = new payfurl.sdk.Vault();
            var result = svc.Delete(vault.VaultId);

            Assert.Equal(result.VaultId, vault.VaultId);
        }

        private VaultData CreateVault()
        {
            var newVault = new NewVault
            {
                CardNumber = "4111111111111111",
                Ccv = "123"
            };
            var svc = new payfurl.sdk.Vault();
            return svc.Create(newVault);
        }
    }
}
