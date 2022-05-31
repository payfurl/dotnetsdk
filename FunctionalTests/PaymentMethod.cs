using payfurl.sdk;
using payfurl.sdk.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FunctionalTests
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Fact]
        public void Checkout()
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
            var result = svc.Checkout(checkout);

            Assert.NotNull(result.CheckoutId);
            Assert.NotNull( result.TransactionId);
        }

        [Fact]
        public void Search()
        {
            var customer = new NewCustomerCard
            {
                FirstName = "test",
                LastName = "test",
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/22",
                    Ccv = "123"
                }
            };

            var customerSvc = new payfurl.sdk.Customer();
            var newCustomer = customerSvc.CreateWithCard(customer);

            var paymentMethodSvc = new payfurl.sdk.PaymentMethod();
            var result = paymentMethodSvc.Search(new PaymentMethodSearch {CustomerId = newCustomer.CustomerId});

            Assert.Equal(1, result.Count);
            Assert.Equal(newCustomer.CustomerId, result.PaymentMethods.First().CustomerId);
        }

        [Fact]
        public void Single()
        {
            var customer = new NewCustomerCard
            {
                FirstName = "test",
                LastName = "test",
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/22",
                    Ccv = "123"
                }
            };

            var customerSvc = new payfurl.sdk.Customer();
            var newCustomer = customerSvc.CreateWithCard(customer);

            var paymentMethodSvc = new payfurl.sdk.PaymentMethod();
            var result = paymentMethodSvc.Single(newCustomer.DefaultPaymentMethod.PaymentMethodId);

            Assert.Equal(newCustomer.DefaultPaymentMethod.PaymentMethodId, result.PaymentMethodId);
        }
    }
}
