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
    }
}
