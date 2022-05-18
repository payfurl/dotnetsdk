using System.Collections.Generic;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface ICustomer
    {
        CustomerData CreateWithCard(NewCustomerCard newCustomer);
        CustomerData CreateWithToken(NewCustomerToken newCustomer);
        CustomerList Search(CustomerSearch searchData);
        CustomerData Single(string customerId);
        List<PaymentMethodData> GetPaymentMethods(string customerId);
    }
}