using evertech.sdk;
using evertech.sdk.Models;
using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class Customer
    {
        [SetUp]
        public void SetConfig()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Test]
        public void SearchWithValidReference()
        {
            var search = new CustomerSearch
            {
                Reference = "123123123"
            };

            var svc = new evertech.sdk.Customer();
            var result = svc.Search(search);

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void SearchWithInvalidKey()
        {
            Config.Setup("invalidkey", Environment.LOCAL);

            var search = new CustomerSearch
            {
                Reference = "123123123"
            };

            var svc = new evertech.sdk.Customer();

            Assert.Throws<ApiException>(() => svc.Search(search));
        }

        [Test]
        public void AddWithCard()
        {
            var customer = new NewCustomerCard
            {
                FirstName = "test",
                LastName = "test",
                ProviderId = "5d9206538fb53f4ac4cda1da",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/22",
                    Ccv = "123"
                }
            };

            var svc = new evertech.sdk.Customer();
            var result = svc.CreateWithCard(customer);

            Assert.IsNotNull(result.CustomerId);
        }

        [Test]
        [Ignore("tokens expire, so this test needs to be adjusted each time it's run")]
        public void AddWithToken()
        {
            var customer = new NewCustomerToken
            {
                FirstName = "test",
                LastName = "test",
                Token = "5dc5d0d4ec7c4d057cb00484"
            };

            var svc = new evertech.sdk.Customer();
            var result = svc.CreateWithToken(customer);

            Assert.IsNotNull(result.CustomerId);
        }
    }
}
