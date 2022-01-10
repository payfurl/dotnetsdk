using payfurl.sdk;
using payfurl.sdk.Models;
using Xunit;

namespace FunctionalTests
{
    public class Charge
    {
        public Charge()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Fact]
        public void ChargeWithValidCard()
        {
            var chargeData = new NewChargeCard
            {
                Amount = 20,
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
		            ExpiryDate = "12/22",
		            Ccv = "123"
                }
            };

            var svc = new payfurl.sdk.Charge();
            var result = svc.CreateWithCard(chargeData);

            Assert.Equal("SUCCESS", result.Status);
        }

        [Fact]
        public void ChargeWithValidCardLeastCost()
        {
            var chargeData = new NewChargeCardLeastCost
            {
                Amount = 20,
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
		            ExpiryDate = "12/22",
		            Ccv = "123"
                }
            };

            var svc = new payfurl.sdk.Charge();
            var result = svc.CreateWithCardLeastCost(chargeData);

            Assert.Equal("SUCCESS", result.Status);
        }


        [Fact]
        public void Search()
        {
            var svc = new payfurl.sdk.Charge();
            var result = svc.Search(new ChargeSearch());

            Assert.Equal(0, result.Skip);
        }

        [Fact]
        public void ChargePaymentMethod()
        {
            var custSvc = new payfurl.sdk.Customer();

            var newCustomer = new NewCustomerCard
            {
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/22",
                    Ccv = "123"
                }
            };
            var createdCustomer = custSvc.CreateWithCard(newCustomer);

            var payMethodSvc = new payfurl.sdk.PaymentMethod();

            var createdPaymentMethod = payMethodSvc.GetForCustomer(createdCustomer.CustomerId);

            var chargeSvc = new payfurl.sdk.Charge();
            var charge = new NewChargePaymentMethod
            {
                Amount = 5,
                PaymentMethodId = createdPaymentMethod[0].PaymentMethodId
            };
            var result = chargeSvc.CreateWitPaymentMethod(charge);

            Assert.Equal("SUCCESS", result.Status);
        }

        [Fact]
        public void Refund()
        {
            var chargeData = new NewChargeCard
            {
                Amount = 20,
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/22",
                    Ccv = "123"
                }
            };

            var svc = new payfurl.sdk.Charge();
            var chargeResult = svc.CreateWithCard(chargeData);

            var refundResult = svc.Refund(new NewRefund { ChargeId = chargeResult.ChargeId });

            Assert.Equal(chargeData.Amount, refundResult.RefundedAmount);
        }

        [Fact(Skip = "tokens expire, so this test needs to be adjusted each time it's run")]
        public void ChargeWithValidToken()
        {
            var chargeData = new NewChargeToken
            {
                Amount = 20,
                Token = "5dc5cfbaec7c4d057cb00482"
            };

            var svc = new payfurl.sdk.Charge();
            var result = svc.CreateWithToken(chargeData);

            Assert.Equal("SUCCESS", result.Status);
        }
    }
}
