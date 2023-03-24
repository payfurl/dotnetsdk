using payfurl.sdk;
using payfurl.sdk.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Environment = payfurl.sdk.Environment;

namespace FunctionalTests
{
    public class PaymentMethod : BaseTest
    {
        private const string ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed";

        private static readonly CardRequestInformation CardRequestInformation = new()
        {
            CardNumber = "4111111111111111",
            ExpiryDate = "12/35",
            Ccv = "123"
        };

        private readonly NewCheckout _checkout = new()
        {
            ProviderId = "1cf5deda-28cc-4214-adb5-1e597a37228c",
            Amount = 123,
            Currency = "AUD",
            Reference = "123123123",
            Options = new Dictionary<string, string>
                { { "HideShipping", "true"} }
        };

        private readonly NewPaymentMethodCard _newPaymentMethod = new()
        {
            ProviderId = ProviderId,
            PaymentInformation = CardRequestInformation
        };

        private readonly NewCustomerCard _newCustomer = new()
        {
            ProviderId = ProviderId,
            PaymentInformation = CardRequestInformation,
            VaultCard = true
        };

        private readonly NewPaymentMethodCard _newPaymentMethodCard = new()
        {
            ProviderId = ProviderId,
            PaymentInformation = CardRequestInformation
        };

        [Fact]
        public void Checkout()
        {
            var svc = new payfurl.sdk.PaymentMethod();
            var result = svc.Checkout(_checkout);

            Assert.NotNull(result.CheckoutId);
            Assert.NotNull(result.TransactionId);
        }

        [Fact]
        public async Task CheckoutAsync()
        {
            var checkout = new NewCheckout
            {
                ProviderId = "1cf5deda-28cc-4214-adb5-1e597a37228c",
                Amount = 123,
                Currency = "AUD",
                Reference = "123123123",
                Options = new Dictionary<string, string>
                { { "HideShipping", "true"} }
            };

            var svc = new payfurl.sdk.PaymentMethod();
            var result = await svc.CheckoutAsync(checkout);

            Assert.NotNull(result.CheckoutId);
            Assert.NotNull(result.TransactionId);
        }

        [Fact]
        public void CreatePaymentMethodWithCard()
        {

            var svc = new payfurl.sdk.PaymentMethod();
            _newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
            var result = svc.CreatePaymentMethodWithCard(_newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public async Task CreatePaymentMethodWithCardAsync()
        {
            var svc = new payfurl.sdk.PaymentMethod();
            var result = await svc.CreatePaymentMethodWithCardAsync(_newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public void Single()
        {
            var svc = new payfurl.sdk.PaymentMethod();
            var paymentMethodWithCard = svc.CreatePaymentMethodWithCard(_newPaymentMethodCard);

            var result = svc.Single(paymentMethodWithCard.PaymentMethodId);

            Assert.Equal(paymentMethodWithCard.PaymentMethodId, result.PaymentMethodId);
        }

        [Fact]
        public async Task SingleAsync()
        {
            var svc = new payfurl.sdk.PaymentMethod();
            var paymentMethodWithCard = await svc.CreatePaymentMethodWithCardAsync(_newPaymentMethodCard);

            var result = await svc.SingleAsync(paymentMethodWithCard.PaymentMethodId);

            Assert.Equal(paymentMethodWithCard.PaymentMethodId, result.PaymentMethodId);
        }

        [Fact]
        public void CreatePaymentMethodWithVault()
        {
            var custSvc = new payfurl.sdk.Customer();
            var customer = custSvc.CreateWithCard(_newCustomer);

            var newPaymentMethod = new NewPaymentMethodVault
            {
                ProviderId = ProviderId,
                PaymentMethodId = customer.DefaultPaymentMethod.PaymentMethodId
            };

            var svc = new payfurl.sdk.PaymentMethod();
            var result = svc.CreatePaymentMethodWithVault(newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public async Task CreatePaymentMethodWithVaultAsync()
        {
            var custSvc = new payfurl.sdk.Customer();
            var customer = await custSvc.CreateWithCardAsync(_newCustomer);

            var newPaymentMethod = new NewPaymentMethodVault
            {
                ProviderId = ProviderId,
                PaymentMethodId = customer.DefaultPaymentMethod.PaymentMethodId
            };

            var svc = new payfurl.sdk.PaymentMethod();
            var result = await svc.CreatePaymentMethodWithVaultAsync(newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public void Search()
        {
            var svc = new payfurl.sdk.PaymentMethod();
            var paymentMethodWithCard = svc.CreatePaymentMethodWithCard(_newPaymentMethod);

            var result = svc.Search(new PaymentMethodSearch { ProviderId = _newPaymentMethod.ProviderId});

            Assert.False(result.Count == 0);
        }

        [Fact]
        public async Task SearchAsync()
        {
            var svc = new payfurl.sdk.PaymentMethod();
            var paymentMethodWithCard = await svc.CreatePaymentMethodWithCardAsync(_newPaymentMethod);

            var result = svc.Search(new PaymentMethodSearch { ProviderId = _newPaymentMethod.ProviderId });

            Assert.False(result.Count == 0);
        }

        public PaymentMethod(ITestOutputHelper output) : base(output)
        {
        }
    }
}
