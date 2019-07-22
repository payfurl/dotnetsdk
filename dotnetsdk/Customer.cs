using evertech.sdk.Models;
using evertech.sdk.Tools;

namespace evertech.sdk
{
    public class Customer
    {
        public CustomerData CreateWithCard(NewCustomerCard newCustomer)
        {
            return HttpWrapper.Call<NewCustomerCard, CustomerData>("/customer/card", Method.POST, newCustomer);
        }
    }
}
