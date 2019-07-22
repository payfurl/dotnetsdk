using evertech.sdk;
using evertech.sdk.Models;
using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class Charge
    {
        [SetUp]
        public void SetConfig()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", evertech.sdk.Environment.LOCAL);
        }

        [Test]
        public void ChargeWithValidCard()
        {
            var chargeData = new NewChargeCard
            {
                Amount = 20,
                ProviderId = "5de3d6dc-2943-4163-a0b0-00b92a63fdeb",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
		            ExpiryDate = "12/22",
		            Ccv = "123"
                }
            };

            var svc = new evertech.sdk.Charge();
            var result = svc.CreateWithCard(chargeData);

            Assert.AreEqual("SUCCESS", result.Status);
        }
    }
}
