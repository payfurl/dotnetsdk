using System.Collections.Generic;
using System.Threading.Tasks;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface ICustomer
    {
        CustomerData CreateWithCard(NewCustomerCard newCustomer);
        Task<CustomerData> CreateWithCardAsync(NewCustomerCard newCustomer);
        CustomerData CreateWithToken(NewCustomerToken newCustomer);
        Task<CustomerData> CreateWithTokenAsync(NewCustomerToken newCustomer);
        CustomerData CreateWithProviderToken(NewCustomerProviderToken newCustomer);
        Task<CustomerData> CreateWithProviderTokenAsync(NewCustomerProviderToken newCustomer);
        PaymentMethodData CreatePaymentMethodWithCard(string customerId, NewPaymentMethodCard newPaymentMethod);
        Task<PaymentMethodData> CreatePaymentMethodWithCardAsync(string customerId, NewPaymentMethodCard newPaymentMethod);
        PaymentMethodData CreatePaymentMethodWithToken(string customerId, NewPaymentMethodToken newPaymentMethod);
        Task<PaymentMethodData> CreatePaymentMethodWithTokenAsync(string customerId, NewPaymentMethodToken newPaymentMethod);
        PaymentMethodData CreatePaymentMethodWithPayTo(string customerId, NewPayToAgreement newPayToAgreement);

        Task<PaymentMethodData> CreatePaymentMethodWithPayToAsync(string customerId,
            NewPayToAgreement newPayToAgreement);
        List<PaymentMethodData> GetPaymentMethods(string customerId);
        Task<List<PaymentMethodData>> GetPaymentMethodsAsync(string customerId);
        CustomerData Single(string customerId);
        Task<CustomerData> SingleAsync(string customerId);
        CustomerList Search(CustomerSearch searchData);
        Task<CustomerList> SearchAsync(CustomerSearch searchData);
        CustomerData Delete(string customerId);
        Task<CustomerData> DeleteAsync(string customerId);
        CustomerData Update(string customerId, UpdateCustomer updateCustomer);
        Task<CustomerData> UpdateAsync(string customerId, UpdateCustomer updateCustomer);
    }
}