using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using payfurl.sdk.Models;
using Xunit;
using Xunit.Abstractions;

namespace FunctionalTests
{
    public class Charge : BaseTest
    {
        private const string SuccessResponseValue = "SUCCESS";

        private static readonly CardRequestInformation CardRequestInformation = new()
        {
            CardNumber = "4111111111111111",
            ExpiryDate = "02/32",
            Ccv = "987",
            Cardholder = "John Smith"
        };

        private NewChargeCard GetChargeData()
        {
            return new NewChargeCard
            {
                Amount = 20,
                ProviderId = GetProviderId(),
                PaymentInformation = CardRequestInformation
            };
        }

        private readonly NewChargeCardLeastCost _newChargeCardLeastCost = new()
        {
            Amount = 20,
            Currency = "AUD",
            PaymentInformation = CardRequestInformation
        };

        private NewCustomerCard GetNewCustomer()
        {
            return new NewCustomerCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = CardRequestInformation
            };
        }

        [Fact]
        public void ChargeWithValidCard()
        {
            var svc = new payfurl.sdk.Charge();
            var chargeData = GetChargeData();
            chargeData.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
            var result = svc.CreateWithCard(chargeData);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public void ChargeWithValidCardWithWebhook()
        {
            var svc = new payfurl.sdk.Charge();

            var chargeData = GetChargeData();
            chargeData.Webhook = new WebhookConfig
            {
                Url = "https://webhook.site/1da8cac9-fef5-47bf-a276-81856f73d7ca",
                Authorization = "Basic user:password"
            };

            var result = svc.CreateWithCard(chargeData);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public async Task ChargeWithValidCardAsync()
        {
            var svc = new payfurl.sdk.Charge();
            var chargeData = GetChargeData();
            var result = await svc.CreateWithCardAsync(chargeData);

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
            var newCustomer = GetNewCustomer();
            var createdCustomer = custSvc.CreateWithCard(newCustomer);

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
            var newCustomer = GetNewCustomer();
            var createdCustomer = await custSvc.CreateWithCardAsync(newCustomer);

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
            var chargeData = GetChargeData();
            var chargeResult = svc.CreateWithCard(chargeData);

            var refundResult = svc.Refund(new NewRefund { ChargeId = chargeResult.ChargeId });

            Assert.Equal(chargeData.Amount, refundResult.RefundedAmount);
        }

        [Fact]
        public async Task RefundAsync()
        {
            var svc = new payfurl.sdk.Charge();
            var chargeData = GetChargeData();
            var chargeResult = await svc.CreateWithCardAsync(chargeData);

            var refundResult = await svc.RefundAsync(new NewRefund { ChargeId = chargeResult.ChargeId });

            Assert.Equal(chargeData.Amount, refundResult.RefundedAmount);
        }

        [Fact]
        public void ChargeWithValidToken()
        {
            var chargeData = new NewChargeToken
            {
                Amount = 20,
                Token = GetPaymentToken()
            };

            var svc = new payfurl.sdk.Charge();
            var result = svc.CreateWithToken(chargeData);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public async Task ChargeWithInvalidCard()
        {
            var svc = new payfurl.sdk.Charge();

            Task Act() => svc.CreateWithCardAsync(new NewChargeCard
            {
                Amount = 20,
                ProviderId = GetProviderId()
            });
            var ex = await Assert.ThrowsAsync<ApiException>(Act);
            
            Assert.Equal(81, ex.Code);
        }

        public Charge(ITestOutputHelper output) : base(output)
        {
        }
    }
}
