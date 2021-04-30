using payfurl.sdk;
using payfurl.sdk.Models;
using System.Collections.Generic;
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
        public void GetForCustomer()
        {
            var search = new CustomerSearch
            {
                Reference = "123123123"
            };

            var svc = new payfurl.sdk.Customer();
            var result = svc.Search(search);

            var customerId = result.Customers[0].CustomerId;

            var payMethodSvc = new payfurl.sdk.PaymentMethod();
            var paymentMethods = payMethodSvc.GetForCustomer(customerId);

            Assert.Single(paymentMethods);
        }

        [Fact]
        public void Checkout()
        {
            var checkout = new NewCheckout
            {
                ProviderId = "5f6339bfbbfb3c0c3c72b884",
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
    }
}
