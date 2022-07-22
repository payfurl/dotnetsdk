using System;
using payfurl.sdk;
using payfurl.sdk.Models;
using System.Collections.Generic;
using Xunit;
using Environment = payfurl.sdk.Environment;

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
        public void CreatePaymentMethodWithCard()
        {
            var newPaymentMethod = new NewPaymentMethodCard
            {
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/22",
                    Ccv = "123"
                }
            };
            var svc = new payfurl.sdk.PaymentMethod();
            var result = svc.CreatePaymentMethodWithCard(newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public void CreatePaymentMethodWithVault()
        {
            var newCustomer = new NewCustomerCard
            {
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/22",
                    Ccv = "123"
                },
                VaultCard = true
            };
            var custSvc = new payfurl.sdk.Customer();
            var customer = custSvc.CreateWithCard(newCustomer);
            
            var newPaymentMethod = new NewPaymentMethodVault
            {
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                PaymentMethodId = customer.DefaultPaymentMethod.PaymentMethodId,
                VaultId = customer.DefaultPaymentMethod.VaultId
            };
            var svc = new payfurl.sdk.PaymentMethod();
            var result = svc.CreatePaymentMethodWithVault(newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);
        }
    }
}
