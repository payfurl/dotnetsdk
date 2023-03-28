using payfurl.sdk.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FunctionalTests
{
    public class PaymentMethod : BaseTest
    {
        private static readonly CardRequestInformation CardRequestInformation = new()
        {
            CardNumber = "4111111111111111",
            ExpiryDate = "12/35",
            Ccv = "123"
        };

        private NewPaymentMethodCard GetNewPaymentMethod()
        {
            return new NewPaymentMethodCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = CardRequestInformation
            };
        }

        private NewCustomerCard GetNewCustomer()
        {
            return new NewCustomerCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = CardRequestInformation,
                VaultCard = true
            };
        }

        private NewPaymentMethodCard GetNewPaymentMethodCard()
        {
            return new NewPaymentMethodCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = CardRequestInformation
            };
        }
        
        [Fact]
        public void CreatePaymentMethodWithCard()
        {

            var svc = new payfurl.sdk.PaymentMethod();
            var newPaymentMethod = GetNewPaymentMethod(); 
            newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
            var result = svc.CreatePaymentMethodWithCard(newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public async Task CreatePaymentMethodWithCardAsync()
        {
            var svc = new payfurl.sdk.PaymentMethod();
            var result = await svc.CreatePaymentMethodWithCardAsync(GetNewPaymentMethod());

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public void Single()
        {
            var svc = new payfurl.sdk.PaymentMethod();
            var paymentMethodWithCard = svc.CreatePaymentMethodWithCard(GetNewPaymentMethodCard());

            var result = svc.Single(paymentMethodWithCard.PaymentMethodId);

            Assert.Equal(paymentMethodWithCard.PaymentMethodId, result.PaymentMethodId);
        }

        [Fact]
        public async Task SingleAsync()
        {
            var svc = new payfurl.sdk.PaymentMethod();
            var paymentMethodWithCard = await svc.CreatePaymentMethodWithCardAsync(GetNewPaymentMethodCard());

            var result = await svc.SingleAsync(paymentMethodWithCard.PaymentMethodId);

            Assert.Equal(paymentMethodWithCard.PaymentMethodId, result.PaymentMethodId);
        }

        [Fact]
        public void CreatePaymentMethodWithVault()
        {
            var custSvc = new payfurl.sdk.Customer();
            var customer = custSvc.CreateWithCard(GetNewCustomer());

            var newPaymentMethod = new NewPaymentMethodVault
            {
                ProviderId = GetProviderId(),
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
            var customer = await custSvc.CreateWithCardAsync(GetNewCustomer());

            var newPaymentMethod = new NewPaymentMethodVault
            {
                ProviderId = GetProviderId(),
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
            var paymentMethodWithCard = svc.CreatePaymentMethodWithCard(GetNewPaymentMethod());

            var result = svc.Search(new PaymentMethodSearch { ProviderId = GetProviderId()});

            Assert.False(result.Count == 0);
        }

        [Fact]
        public async Task SearchAsync()
        {
            var svc = new payfurl.sdk.PaymentMethod();
            var paymentMethodWithCard = await svc.CreatePaymentMethodWithCardAsync(GetNewPaymentMethod());

            var result = svc.Search(new PaymentMethodSearch { ProviderId = GetProviderId() });

            Assert.False(result.Count == 0);
        }
    }
}
