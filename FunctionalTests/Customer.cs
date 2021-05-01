﻿using payfurl.sdk;
using payfurl.sdk.Models;
using Xunit;

namespace FunctionalTests
{
    public class Customer
    {
        public Customer()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Fact]
        public void SearchWithValidReference()
        {
            var search = new CustomerSearch
            {
                Reference = "123123123"
            };

            var svc = new payfurl.sdk.Customer();
            var result = svc.Search(search);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void SearchWithInvalidKey()
        {
            Config.Setup("invalidkey", Environment.LOCAL);

            var search = new CustomerSearch
            {
                Reference = "123123123"
            };

            var svc = new payfurl.sdk.Customer();

            Assert.Throws<ApiException>(() => svc.Search(search));
        }
        
        [Fact]
        public void AddWithCard()
        {
            var customer = new NewCustomerCard
            {
                FirstName = "test",
                LastName = "test",
                ProviderId = "5deae6c2d560623e10966b91",
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/22",
                    Ccv = "123"
                }
            };

            var svc = new payfurl.sdk.Customer();
            var result = svc.CreateWithCard(customer);

            Assert.NotNull(result.CustomerId);
        }

        [Fact(Skip = "tokens expire, so this test needs to be adjusted each time it's run")]
        public void AddWithToken()
        {
            var customer = new NewCustomerToken
            {
                FirstName = "test",
                LastName = "test",
                Token = "5dc5d0d4ec7c4d057cb00484"
            };

            var svc = new payfurl.sdk.Customer();
            var result = svc.CreateWithToken(customer);

            Assert.NotNull(result.CustomerId);
        }
    }
}
