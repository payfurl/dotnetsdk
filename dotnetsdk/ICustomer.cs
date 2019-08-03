using evertech.sdk.Models;

namespace evertech.sdk
{
    public interface ICustomer
    {
        CustomerData CreateWithCard(NewCustomerCard newCustomer);
        CustomerList Search(CustomerSearch searchData);
    }
}