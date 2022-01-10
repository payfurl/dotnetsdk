using payfurl.sdk;
using payfurl.sdk.Models;
using System.Linq;
using Xunit;

namespace FunctionalTests
{
    public class Transfer
    {
        public Transfer()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Fact]
        public void Create()
        {
            var transferData = new NewTransferGroup
            {
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                Transfers = new System.Collections.Generic.List<NewTransfer>
                {
                    new NewTransfer { Amount = 1.25M, Currency = "AUD", Account = "david@uberconcept.com"},
                    new NewTransfer { Amount = 3.25M, Currency = "AUD", Account = "david@uberconcept.com"}
                }
            };

            var svc = new payfurl.sdk.Transfer();
            var result = svc.Create(transferData);

            Assert.Equal("PENDING", result.First().Status);
        }

        [Fact]
        public void Single()
        {
            var transferData = new NewTransferGroup
            {
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                Transfers = new System.Collections.Generic.List<NewTransfer>
                {
                    new NewTransfer { Amount = 1.25M, Currency = "AUD", Account = "david@uberconcept.com"},
                    new NewTransfer { Amount = 3.25M, Currency = "AUD", Account = "david@uberconcept.com"}
                }
            };

            var svc = new payfurl.sdk.Transfer();
            var transfer = svc.Create(transferData);

            var result = svc.Single(transfer.First().TransferId);

            Assert.Equal(result.TransferId, transfer.First().TransferId);
        }

        [Fact]
        public void Search()
        {
            var transferData = new NewTransferGroup
            {
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                Transfers = new System.Collections.Generic.List<NewTransfer>
                {
                    new NewTransfer { Amount = 1.25M, Currency = "AUD", Account = "david@uberconcept.com"},
                    new NewTransfer { Amount = 3.25M, Currency = "AUD", Account = "david@uberconcept.com"}
                }
            };

            var svc = new payfurl.sdk.Transfer();
            var transfer = svc.Create(transferData);

            var result = svc.Search(new TransferSearch { ProviderId = transferData.ProviderId } );

            Assert.Equal(0, result.Skip);
        }
    }
}
