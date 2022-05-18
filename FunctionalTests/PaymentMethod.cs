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
    }
}
