using System.Net.Http;
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
        public void ChargeWithValidCardWithWebhook()
        {
            var svc = new payfurl.sdk.Charge();

            _chargeData.Webhook = new WebhookConfig
            {
                Url = "https://webhook.site/1da8cac9-fef5-47bf-a276-81856f73d7ca",
                Authorization = "Basic user:password"
            };

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

        [Fact]
        public async Task ChargeWithInvalidCard()
        {
            var svc = new payfurl.sdk.Charge();
            
            Task Act() => svc.CreateWithCardAsync(new NewChargeCard());
            var ex = await Assert.ThrowsAsync<ApiException>(Act);

            Assert.Equal(90, ex.Code);
        }

        [Theory]
        [InlineData("rDYP2MxMKvvmoV2KrbOi4pnelHnVJoFYdBegvCK7IQk=", true)]
        [InlineData("InvalidSignature", false)]
        public async Task VerifySignatureForWebhook(string value, bool expected)
        {
            const string webhookSignatureKey = "dCM6l9ngZMJXVappk73yS607k1K7byfyzTTdToaKMa8=";

            var request = new HttpRequestMessage(HttpMethod.Get, "https://webhook.com");
            request.Headers.Add("X-Payfurl-Signature", value);
            request.Content = new StringContent("{\"data\":{\"chargeId\":\"3f83ab8fdf624c649bc70bbba81d6c2b\",\"providerChargeId\":\"ch_3MYd2tE9mXU4onpB0r5iTsiL\",\"amount\":20,\"providerId\":\"a26c371f-94f6-40da-add2-28ec8e9da8ed\",\"paymentInformation\":{\"paymentMethodId\":\"80da8c2d674b4d2e8c65a6520e89d070\",\"card\":{\"cardNumber\":\"4111********1111\",\"expiryDate\":\"12/25\",\"type\":\"VISA\",\"cardType\":\"CREDIT\",\"cardIin\":\"411\"},\"type\":\"CARD\"},\"customerId\":\"025c73d9cd0540e9a5a997f8ba97c732\",\"status\":\"SUCCESS\",\"dateAdded\":\"2023-02-06T22:20:19.0461561Z\",\"successDate\":\"2023-02-06T22:20:20.8655832Z\",\"estimatedCost\":0.20,\"estimatedCostCurrency\":\"AUD\",\"currency\":\"Aud\",\"refunds\":[],\"threeDsVerified\":false},\"meta\":{\"messageId\":\"bc4f056315d6e0205ab085dde45c4a46\",\"timestamp\":\"2023-01-19T20:37:12.8456589Z\",\"type\":\"transaction\",\"eventType\":\"transaction.status.changed\"}}");

            var isFromPayFurl = await payfurl.sdk.Charge.IsFromPayFurl(request, webhookSignatureKey);

            Assert.Equal(isFromPayFurl, expected);
        }
    }
}
