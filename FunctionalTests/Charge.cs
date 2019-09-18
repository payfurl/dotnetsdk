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
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Test]
        public void ChargeWithValidCard()
        {
            var chargeData = new NewChargeCard
            {
                Amount = 20,
                ProviderId = "58bcedf43c541b5b87f73935",
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


        [Test]
        public void Search()
        {
            var svc = new evertech.sdk.Charge();
            var result = svc.Search(new ChargeSearch());

            Assert.AreEqual(0, result.Skip);
        }

        [Test]
        public void ChargePaymentMethod()
        {
            var custSvc = new evertech.sdk.Customer();

            var newCustomer = new NewCustomerCard
            {
                ProviderId = "58bcedf43c541b5b87f73935",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/22",
                    Ccv = "123"
                }
            };
            var createdCustomer = custSvc.CreateWithCard(newCustomer);

            var payMethodSvc = new evertech.sdk.PaymentMethod();

            var createdPaymentMethod = payMethodSvc.GetForCustomer(createdCustomer.CustomerId);

            var chargeSvc = new evertech.sdk.Charge();
            var charge = new NewChargePaymentMethod
            {
                Amount = 5,
                PaymentMethodId = createdPaymentMethod[0].PaymentMethodId
            };
            var result = chargeSvc.CreateWitPaymentMethod(charge);

            Assert.AreEqual("SUCCESS", result.Status);
        }
    }
}
