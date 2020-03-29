using payfurl.sdk;
using payfurl.sdk.Models;
using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class PaymentMethod
    {
        [SetUp]
        public void SetConfig()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Test]
        public void GetForCustomer()
        {
            var search = new CustomerSearch
            {
                Reference = "123123123"
            };

            var svc = new payfurl.sdk.Customer();
            var result = svc.Search(search);

            var customerId = result.Customers[0].CustomerId;

            var payMethodSvc = new payfurl.sdk.PaymentMethod();
            var paymentMethods = payMethodSvc.GetForCustomer(customerId);

            Assert.AreEqual(1, paymentMethods.Count);
        }
    }
}
