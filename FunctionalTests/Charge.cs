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
                ProviderId = "5deae6c2d560623e10966b91",
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
                ProviderId = "5deae6c2d560623e10966b92",
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

        [Test]
        public void Refund()
        {
            var chargeData = new NewChargeCard
            {
                Amount = 20,
                ProviderId = "5deae6c2d560623e10966b91",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/22",
                    Ccv = "123"
                }
            };

            var svc = new evertech.sdk.Charge();
            var chargeResult = svc.CreateWithCard(chargeData);

            var refundResult = svc.Refund(new NewRefund { ChargeId = chargeResult.ChargeId });

            Assert.AreEqual(chargeData.Amount, refundResult.RefundedAmount);
        }

        [Test]
        [Ignore("tokens expire, so this test needs to be adjusted each time it's run")]
        public void ChargeWithValidToken()
        {
            var chargeData = new NewChargeToken
            {
                Amount = 20,
                Token = "5dc5cfbaec7c4d057cb00482"
            };

            var svc = new evertech.sdk.Charge();
            var result = svc.CreateWithToken(chargeData);

            Assert.AreEqual("SUCCESS", result.Status);
        }
    }
}
