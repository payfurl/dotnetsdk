using System.Threading.Tasks;
using payfurl.sdk;
using payfurl.sdk.Models;
using Xunit;

namespace FunctionalTests
{
    public class Charge
    {
        private const string SuccessResponseValue = "SUCCESS";
        private const string ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed";

        private static readonly CardRequestInformation CardRequestInformation = new()
        {
            CardNumber = "4111111111111111",
            ExpiryDate = "12/35",
            Ccv = "123"
        };

        private readonly NewChargeCard _chargeData = new()
        {
            Amount = 20,
            ProviderId = ProviderId,
            PaymentInformation = CardRequestInformation
        };

        private readonly NewChargeCardLeastCost _newChargeCardLeastCost = new()
        {
            Amount = 20,
            PaymentInformation = CardRequestInformation
        };

        private readonly NewCustomerCard _newCustomer = new()
        {
            ProviderId = ProviderId,
            PaymentInformation = CardRequestInformation
        };

        public Charge()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Fact]
        public void ChargeWithValidCard()
        {
            var svc = new payfurl.sdk.Charge();
            var result = svc.CreateWithCard(_chargeData);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public async Task ChargeWithValidCardAsync()
        {
            var svc = new payfurl.sdk.Charge();
            var result = await svc.CreateWithCardAsync(_chargeData);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public void ChargeWithValidCardLeastCost()
        {
            var svc = new payfurl.sdk.Charge();
            var result = svc.CreateWithCardLeastCost(_newChargeCardLeastCost);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public async Task ChargeWithValidCardLeastCostAsync()
        {
            var svc = new payfurl.sdk.Charge();
            var result = await svc.CreateWithCardLeastCostAsync(_newChargeCardLeastCost);

            Assert.Equal(SuccessResponseValue, result.Status);
        }


        [Fact]
        public void Search()
        {
            var svc = new payfurl.sdk.Charge();
            var result = svc.Search(new ChargeSearch());

            Assert.Equal(0, result.Skip);
        }

        [Fact]
        public async Task SearchAsync()
        {
            var svc = new payfurl.sdk.Charge();
            var result = await svc.SearchAsync(new ChargeSearch());

            Assert.Equal(0, result.Skip);
        }

        [Fact]
        public void ChargePaymentMethod()
        {
            var custSvc = new payfurl.sdk.Customer();
            var createdCustomer = custSvc.CreateWithCard(_newCustomer);

            var createdPaymentMethod = custSvc.GetPaymentMethods(createdCustomer.CustomerId);

            var chargeSvc = new payfurl.sdk.Charge();
            var charge = new NewChargePaymentMethod
            {
                Amount = 5,
                PaymentMethodId = createdPaymentMethod[0].PaymentMethodId
            };

            var result = chargeSvc.CreateWithPaymentMethod(charge);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public async Task ChargePaymentMethodAsync()
        {
            var custSvc = new payfurl.sdk.Customer();
            var createdCustomer = await custSvc.CreateWithCardAsync(_newCustomer);

            var createdPaymentMethod = await custSvc.GetPaymentMethodsAsync(createdCustomer.CustomerId);

            var chargeSvc = new payfurl.sdk.Charge();
            var charge = new NewChargePaymentMethod
            {
                Amount = 5,
                PaymentMethodId = createdPaymentMethod[0].PaymentMethodId
            };

            var result = await chargeSvc.CreateWithPaymentMethodAsync(charge);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public void Refund()
        {
            var svc = new payfurl.sdk.Charge();
            var chargeResult = svc.CreateWithCard(_chargeData);

            var refundResult = svc.Refund(new NewRefund { ChargeId = chargeResult.ChargeId });

            Assert.Equal(_chargeData.Amount, refundResult.RefundedAmount);
        }

        [Fact]
        public async Task RefundAsync()
        {
            var svc = new payfurl.sdk.Charge();
            var chargeResult = await svc.CreateWithCardAsync(_chargeData);

            var refundResult = await svc.RefundAsync(new NewRefund { ChargeId = chargeResult.ChargeId });

            Assert.Equal(_chargeData.Amount, refundResult.RefundedAmount);
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

            Assert.Equal(SuccessResponseValue, result.Status);
        }
    }
}
