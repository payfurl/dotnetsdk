using evertech.sdk;
using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class Charge
    {
        [SetUp]
        public void SetConfig()
        {
            Config.Setup("<insertSecretKey>", evertech.sdk.Environment.SANDBOX);
        }

        [Test]
        public void ChargeWithValidCard()
        {
            // TODO: implement body
        }
    }
}
