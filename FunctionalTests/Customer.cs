using System;
using System.Threading.Tasks;
using payfurl.sdk;
using payfurl.sdk.Models;
using Xunit;
using Environment = payfurl.sdk.Environment;

namespace FunctionalTests
{
    public class Customer
    {
        private readonly CustomerSearch _search = new()
        {
            Reference = "123123123"
        };

        private readonly NewPaymentMethodCard _paymentMethod = new()
        {
            ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
            PaymentInformation = new CardRequestInformation
            {
                CardNumber = "4111111111111111",
                ExpiryDate = "12/35",
                Ccv = "123"
            }
        };
        
        private readonly NewPaymentMethodCard _paymentMethodSetDefault = new()
        {
            ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
            PaymentInformation = new CardRequestInformation
            {
                CardNumber = "4111111111111111",
                ExpiryDate = "12/35",
                Ccv = "123"
            },
            SetDefault = true
        };
        
        private readonly NewPayToAgreement _payToSetDefault = new()
        {
            ProviderId = "ec422274fe6d4a6e9f54157381603740",
            PayerName = "This is a name",
            Description = "This is a description",
            MaximumAmount = 500,
            PayerPayIdDetails = new NewPayToAgreement.PayIdDetails()
            {
                PayId = "david_jones@email.com",
                PayIdType = "EMAIL"
            },
            SetDefault = true
        };
        
        private readonly NewPayToAgreement _payTo = new()
        {
            ProviderId = "ec422274fe6d4a6e9f54157381603740",
            PayerName = "This is a name",
            Description = "This is a description",
            MaximumAmount = 500,
            PayerPayIdDetails = new NewPayToAgreement.PayIdDetails()
            {
                PayId = "david_jones@email.com",
                PayIdType = "EMAIL"
            }
        };

        private static NewCustomerCard CreateNewCustomerCard(string reference = "")
        {
            return new NewCustomerCard
            {
                FirstName = "test",
                LastName = "test",
                ProviderId = "ec422274fe6d4a6e9f54157381603740",
                Reference = reference,
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/35",
                    Ccv = "123"
                }
            };
        }

        public Customer()
        {
            Config.Setup("secteste760dc4185ba394e6148e2612b644de493cd068aa6", Environment.LOCAL);
        }

        [Fact]
        public void SearchWithValidReference()
        {
            var reference = Guid.NewGuid().ToString();

            var customer = CreateNewCustomerCard(reference);
            var svc = new payfurl.sdk.Customer();
            svc.CreateWithCard(customer);
            
            var search = new CustomerSearch
            {
                Reference = reference
            };

            var result = svc.Search(search);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task SearchWithValidReferenceAsync()
        {
            var reference = Guid.NewGuid().ToString();

            var customer = CreateNewCustomerCard(reference);
            var svc = new payfurl.sdk.Customer();
            await svc.CreateWithCardAsync(customer);

            var search = new CustomerSearch
            {
                Reference = reference
            };

            var result = await svc.SearchAsync(search);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void SearchWithInvalidKey()
        {
            Config.Setup("invalidkey", Environment.LOCAL);

            var svc = new payfurl.sdk.Customer();

            Assert.Throws<ApiException>(() => svc.Search(_search));
        }

        [Fact]
        public async Task SearchWithInvalidKeyAsync()
        {
            Config.Setup("invalidkey", Environment.LOCAL);

            var svc = new payfurl.sdk.Customer();

            await Assert.ThrowsAsync<ApiException>(() => svc.SearchAsync(_search));
        }
        
        [Fact]
        public void AddWithCard()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var result = svc.CreateWithCard(customer);

            Assert.NotNull(result.CustomerId);
        }

        [Fact]
        public async Task AddWithCardAsync()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var result = await svc.CreateWithCardAsync(customer);

            Assert.NotNull(result.CustomerId);
        }

        [Fact]
        public void AddPaymentMethodWithCard()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithCard(customer);

            var result = svc.CreatePaymentMethodWithCard(newCustomer.CustomerId, _paymentMethod);
            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public async Task AddPaymentMethodWithCardAsync()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = await svc.CreateWithCardAsync(customer);

            var result = await svc.CreatePaymentMethodWithCardAsync(newCustomer.CustomerId, _paymentMethod);
            Assert.NotNull(result.PaymentMethodId);
        }
        
        [Fact]
        public void AddPaymentMethodWithCardSetDefault()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithCard(customer);

            var result = svc.CreatePaymentMethodWithCard(newCustomer.CustomerId, _paymentMethodSetDefault);
            Assert.NotNull(result.PaymentMethodId);

            var updatedCustomer = svc.Single(newCustomer.CustomerId);
            Assert.NotEqual(newCustomer.DefaultPaymentMethod.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
        }

        [Fact]
        public async Task AddPaymentMethodWithCardSetDefaultAsync()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = await svc.CreateWithCardAsync(customer);

            var result = await svc.CreatePaymentMethodWithCardAsync(newCustomer.CustomerId, _paymentMethodSetDefault);
            Assert.NotNull(result.PaymentMethodId);
            
            var updatedCustomer = await svc.SingleAsync(newCustomer.CustomerId);
            Assert.NotEqual(newCustomer.DefaultPaymentMethod.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
        }

        [Fact(Skip = "tokens expire, so this test needs to be adjusted each time it's run")]
        public void AddPaymentMethodWithToken()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithCard(customer);

            var paymentMethod = new NewPaymentMethodToken()
            {
                Token = "4f0fb10355224034a1df949852de34e1"
            };
            
            var result = svc.CreatePaymentMethodWithToken(newCustomer.CustomerId, paymentMethod);
            Assert.NotNull(result.PaymentMethodId);
        }
        
        [Fact(Skip = "tokens expire, so this test needs to be adjusted each time it's run")]
        public void AddPaymentMethodWithTokenSetDefault()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithCard(customer);

            var paymentMethod = new NewPaymentMethodToken()
            {
                Token = "5512cf49df66482fada5cbcc5ddbf873",
                SetDefault = true
            };
            
            var result = svc.CreatePaymentMethodWithToken(newCustomer.CustomerId, paymentMethod);
            Assert.NotNull(result.PaymentMethodId);
            
            var updatedCustomer = svc.Single(newCustomer.CustomerId);
            Assert.NotEqual(newCustomer.DefaultPaymentMethod.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
        }
        
        [Fact]
        public void AddPaymentMethodWithPayTo()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithCard(customer);

            var result = svc.CreatePaymentMethodWithPayTo(newCustomer.CustomerId, _payTo);
            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public async Task AddPaymentMethodWithPayToAsync()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = await svc.CreateWithCardAsync(customer);

            var result = await svc.CreatePaymentMethodWithPayToAsync(newCustomer.CustomerId, _payTo);
            Assert.NotNull(result.PaymentMethodId);
        }
        
        [Fact]
        public void AddPaymentMethodWithPayToSetDefault()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithCard(customer);

            var result = svc.CreatePaymentMethodWithPayTo(newCustomer.CustomerId, _payToSetDefault);
            Assert.NotNull(result.PaymentMethodId);

            var updatedCustomer = svc.Single(newCustomer.CustomerId);
            Assert.NotEqual(newCustomer.DefaultPaymentMethod.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
        }

        [Fact]
        public async Task AddPaymentMethodWithPayToSetDefaultAsync()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = await svc.CreateWithCardAsync(customer);

            var result = await svc.CreatePaymentMethodWithPayToAsync(newCustomer.CustomerId, _payToSetDefault);
            Assert.NotNull(result.PaymentMethodId);
            
            var updatedCustomer = await svc.SingleAsync(newCustomer.CustomerId);
            Assert.NotEqual(newCustomer.DefaultPaymentMethod.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
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

        [Fact]
        public void GetPaymentMethods()
        {
            var reference = Guid.NewGuid().ToString();
            var customer = CreateNewCustomerCard(reference);

            var svc = new payfurl.sdk.Customer();
            svc.CreateWithCard(customer);
            
            var search = new CustomerSearch
            {
                Reference = reference
            };

            var result = svc.Search(search);

            var customerId = result.Customers[0].CustomerId;

            var paymentMethods = svc.GetPaymentMethods(customerId);

            Assert.Single(paymentMethods);
        }

        [Fact]
        public async Task GetPaymentMethodsAsync()
        {
            var reference = Guid.NewGuid().ToString();
            var customer = CreateNewCustomerCard(reference);

            var svc = new payfurl.sdk.Customer();
            await svc.CreateWithCardAsync(customer);

            var search = new CustomerSearch
            {
                Reference = reference
            };

            var result = await svc.SearchAsync(search);

            var customerId = result.Customers[0].CustomerId;

            var paymentMethods = await svc.GetPaymentMethodsAsync(customerId);

            Assert.Single(paymentMethods);
        }

        [Fact]
        public void Single()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var result = svc.CreateWithCard(customer);

            var newCustomer = svc.Single(result.CustomerId);
            
            Assert.Equal(newCustomer.CustomerId, result.CustomerId);
            Assert.Equal(newCustomer.FirstName, result.FirstName);
            Assert.Equal(newCustomer.LastName, result.LastName);
        }

        [Fact]
        public async Task SingleAsync()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var result = await svc.CreateWithCardAsync(customer);

            var newCustomer = await svc.SingleAsync(result.CustomerId);

            Assert.Equal(newCustomer.CustomerId, result.CustomerId);
            Assert.Equal(newCustomer.FirstName, result.FirstName);
            Assert.Equal(newCustomer.LastName, result.LastName);
        }
        
        [Fact]
        public void AddWithProviderToken()
        {
            var customer = CreateNewCustomerProviderToken();

            var svc = new payfurl.sdk.Customer();
            var result = svc.CreateWithProviderToken(customer);

            Assert.NotNull(result.CustomerId);
        }

        [Fact]
        public async Task AddWithProviderTokenAsync()
        {
            var customer = CreateNewCustomerProviderToken();

            var svc = new payfurl.sdk.Customer();
            var result = await svc.CreateWithProviderTokenAsync(customer);

            Assert.NotNull(result.CustomerId);
        }
        
        [Fact]
        public void UpdateCustomerDefaultPaymentMethodId()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithCard(customer);

            // Do not set as default here
            var result = svc.CreatePaymentMethodWithCard(newCustomer.CustomerId, _paymentMethod);
            Assert.NotNull(result.PaymentMethodId);

            var updatedCustomer = svc.Single(newCustomer.CustomerId);
            Assert.Equal(newCustomer.DefaultPaymentMethod.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
            Assert.NotEqual(result.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);

            // Update Customer's DefaultPaymentMethodId
            svc.Update(newCustomer.CustomerId, new UpdateCustomer() { DefaultPaymentMethodId = result.PaymentMethodId });
            
            updatedCustomer = svc.Single(newCustomer.CustomerId);
            Assert.NotEqual(newCustomer.DefaultPaymentMethod.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
        }

        private static NewCustomerProviderToken CreateNewCustomerProviderToken()
        {
            var customer = new NewCustomerProviderToken
            {
                ProviderId = "a26c371f-94f6-40da-add2-28ec8e9da8ed",
                ProviderToken = "123"
            };
            return customer;
        }
    }
}
