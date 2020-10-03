using payfurl.sdk;
using payfurl.sdk.Models;
using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class Transfer
    {
        [SetUp]
        public void SetConfig()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Test]
        public void Create()
        {
            var transferData = new NewTransfer
            {
                ProviderId = "5f6339bfbbfb3c0c3c72b884",
                Transfers = new System.Collections.Generic.List<TransferItem>
                {
                    new TransferItem { Amount = 1.25M, Currency = "AUD", Account = "david@uberconcept.com"},
                    new TransferItem { Amount = 3.25M, Currency = "AUD", Account = "david@uberconcept.com"}
                }
            };

            var svc = new payfurl.sdk.Transfer();
            var result = svc.Create(transferData);

            Assert.AreEqual("PENDING", result.Status);
        }

        [Test]
        public void Single()
        {
            var transferData = new NewTransfer
            {
                ProviderId = "5f6339bfbbfb3c0c3c72b884",
                Transfers = new System.Collections.Generic.List<TransferItem>
                {
                    new TransferItem { Amount = 1.25M, Currency = "AUD", Account = "david@uberconcept.com"},
                    new TransferItem { Amount = 3.25M, Currency = "AUD", Account = "david@uberconcept.com"}
                }
            };

            var svc = new payfurl.sdk.Transfer();
            var transfer = svc.Create(transferData);

            var result = svc.Single(transfer.TransferId);

            Assert.AreEqual(result.TransferId, transfer.TransferId);
        }

        [Test]
        public void Search()
        {
            var transferData = new NewTransfer
            {
                ProviderId = "5f6339bfbbfb3c0c3c72b884",
                Transfers = new System.Collections.Generic.List<TransferItem>
                {
                    new TransferItem { Amount = 1.25M, Currency = "AUD", Account = "david@uberconcept.com"},
                    new TransferItem { Amount = 3.25M, Currency = "AUD", Account = "david@uberconcept.com"}
                }
            };

            var svc = new payfurl.sdk.Transfer();
            var transfer = svc.Create(transferData);

            var result = svc.Search(new TransferSearch { ProviderId = transferData.ProviderId } );

            Assert.AreEqual(0, result.Skip);
        }
    }
}
