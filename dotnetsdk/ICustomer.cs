using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface ICustomer
    {
        CustomerData CreateWithCard(NewCustomerCard newCustomer);
        CustomerList Search(CustomerSearch searchData);
    }
}