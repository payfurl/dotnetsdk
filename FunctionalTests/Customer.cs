using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using payfurl.sdk;
using payfurl.sdk.Models;
using Xunit;
using Environment = payfurl.sdk.Environment;

namespace FunctionalTests
{
    public class Customer : BaseTest
    {
        private readonly CustomerSearch _search = new()
        {
            Reference = "123123123"
        };

        private NewPaymentMethodCard GetPaymentMethod()
        {
            return new NewPaymentMethodCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/35",
                    Ccv = "123"
                }
            };
        }

        private NewPaymentMethodCard GetPaymentMethodSetDefault()
        {
            return new NewPaymentMethodCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/35",
                    Ccv = "123"
                },
                SetDefault = true
            };
        }

        private NewPayToAgreement GetPayToSetDefault()
        {
            return new NewPayToAgreement
            {
                ProviderId = GetProviderId(),
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
        }

        private NewPayToAgreement GetPayTo()
        {
            return new NewPayToAgreement
            {
                ProviderId = GetProviderId(),
                PayerName = "This is a name",
                Description = "This is a description",
                MaximumAmount = 500,
                PayerPayIdDetails = new NewPayToAgreement.PayIdDetails()
                {
                    PayId = "david_jones@email.com",
                    PayIdType = "EMAIL"
                }
            };
        }

        private NewCustomerCard CreateNewCustomerCard(string reference = "")
        {
            return new NewCustomerCard
            {
                FirstName = "test",
                LastName = "test",
                ProviderId = GetProviderId(),
                Reference = reference,
                PaymentInformation = new CardRequestInformation
                {
                    CardNumber = "4111111111111111",
                    ExpiryDate = "12/35",
                    Ccv = "123"
                }
            };
        }

        [Fact]
        public void SearchWithValidReference()
        {
            var reference = Guid.NewGuid().ToString();

            var customer = CreateNewCustomerCard(reference);
            customer.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
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

            var result = svc.CreatePaymentMethodWithCard(newCustomer.CustomerId, GetPaymentMethod());
            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public async Task AddPaymentMethodWithCardAsync()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = await svc.CreateWithCardAsync(customer);

            var result = await svc.CreatePaymentMethodWithCardAsync(newCustomer.CustomerId, GetPaymentMethod());
            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public void AddPaymentMethodWithCardSetDefault()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithCard(customer);

            var result = svc.CreatePaymentMethodWithCard(newCustomer.CustomerId, GetPaymentMethodSetDefault());
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

            var result = await svc.CreatePaymentMethodWithCardAsync(newCustomer.CustomerId, GetPaymentMethodSetDefault());
            Assert.NotNull(result.PaymentMethodId);

            var updatedCustomer = await svc.SingleAsync(newCustomer.CustomerId);
            Assert.NotEqual(newCustomer.DefaultPaymentMethod.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
        }

        [Fact]
        public void AddPaymentMethodWithToken()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithCard(customer);

            var paymentMethod = new NewPaymentMethodToken()
            {
                Token = GetPaymentToken()
            };

            var result = svc.CreatePaymentMethodWithToken(newCustomer.CustomerId, paymentMethod);
            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public void AddPaymentMethodWithTokenSetDefault()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithCard(customer);

            var paymentMethod = new NewPaymentMethodToken()
            {
                Token = GetPaymentToken(),
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

            var result = svc.CreatePaymentMethodWithPayTo(newCustomer.CustomerId, GetPayTo());
            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public async Task AddPaymentMethodWithPayToAsync()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = await svc.CreateWithCardAsync(customer);

            var result = await svc.CreatePaymentMethodWithPayToAsync(newCustomer.CustomerId, GetPayTo());
            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public void AddPaymentMethodWithPayToSetDefault()
        {
            var customer = CreateNewCustomerCard();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithCard(customer);

            var result = svc.CreatePaymentMethodWithPayTo(newCustomer.CustomerId, GetPayToSetDefault());
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

            var result = await svc.CreatePaymentMethodWithPayToAsync(newCustomer.CustomerId, GetPayToSetDefault());
            Assert.NotNull(result.PaymentMethodId);

            var updatedCustomer = await svc.SingleAsync(newCustomer.CustomerId);
            Assert.NotEqual(newCustomer.DefaultPaymentMethod.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, updatedCustomer.DefaultPaymentMethod.PaymentMethodId);
        }

        [Fact]
        public void AddWithToken()
        {
            var customer = new NewCustomerToken
            {
                FirstName = "test",
                LastName = "test",
                Token = GetPaymentToken()
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

        [Fact(Skip = "this is for manual testing only")]
        public void AddWithProviderToken()
        {
            var customer = CreateNewCustomerProviderToken();

            var svc = new payfurl.sdk.Customer();
            var result = svc.CreateWithProviderToken(customer);

            Assert.NotNull(result.CustomerId);
        }

        [Fact(Skip = "this is for manual testing only")]
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
            var result = svc.CreatePaymentMethodWithCard(newCustomer.CustomerId, GetPaymentMethod());
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

        private NewCustomerProviderToken CreateNewCustomerProviderToken()
        {
            var customer = new NewCustomerProviderToken
            {
                ProviderId = GetProviderId(),
                ProviderToken = "123"
            };
            return customer;
        }
        
        [Fact]
        public void AddWithBankAccount()
        {
            var customer = CreateNewCustomerBankAccount();

            var svc = new payfurl.sdk.Customer();
            var result = svc.CreateWithBankAccount(customer);

            Assert.NotNull(result.CustomerId);
        }

        [Fact]
        public async Task AddWithBankAccountAsync()
        {
            var customer = CreateNewCustomerBankAccount();

            var svc = new payfurl.sdk.Customer();
            var result = await svc.CreateWithBankAccountAsync(customer);

            Assert.NotNull(result.CustomerId);
        }
        
        private NewCustomerBankPayment CreateNewCustomerBankAccount(string reference = "")
        {
            return new NewCustomerBankPayment
            {
                ProviderId = GetProviderId(),
                Reference = reference,
                BankPaymentInformation = new NewBankPayment()
                {
                    BankCode = "123-456",
                    AccountNumber = "123456",
                    AccountName = "Bank Account"
                }
            };
        }
        
        [Fact]
        public void AddPaymentMethodWithBankAccount()
        {
            var customer = CreateNewCustomerBankAccount();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = svc.CreateWithBankAccount(customer);

            var result = svc.CreatePaymentMethodWithBankAccount(newCustomer.CustomerId, GetPaymentMethodWithBankAccount());
            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public async Task AddPaymentMethodWithBankAccountAsync()
        {
            var customer = CreateNewCustomerBankAccount();

            var svc = new payfurl.sdk.Customer();
            var newCustomer = await svc.CreateWithBankAccountAsync(customer);

            var result = await svc.CreatePaymentMethodWithBankAccountAsync(newCustomer.CustomerId, GetPaymentMethodWithBankAccount());
            Assert.NotNull(result.PaymentMethodId);
        }
        
        private NewPaymentMethodBankPayment GetPaymentMethodWithBankAccount()
        {
            return new NewPaymentMethodBankPayment
            {
                ProviderId = GetProviderId(),
                BankPaymentInformation = new NewBankPayment()
                {
                    BankCode = "123-456",
                    AccountNumber = "123456",
                    AccountName = "Bank Account"
                }
            };
        }
    }
}
