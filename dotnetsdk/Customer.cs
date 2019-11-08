using evertech.sdk.Models;
using evertech.sdk.Tools;
using System.Web;

namespace evertech.sdk
{
    public class Customer : ICustomer
    {
        public CustomerData CreateWithCard(NewCustomerCard newCustomer)
        {
            return HttpWrapper.Call<NewCustomerCard, CustomerData>("/customer/card", Method.POST, newCustomer);
        }
        public CustomerData CreateWithToken(NewCustomerToken newCustomer)
        {
            return HttpWrapper.Call<NewCustomerToken, CustomerData>("/customer/token", Method.POST, newCustomer);
        }

        public CustomerList Search(CustomerSearch searchData)
        {
            // TODO: move into a shared class
            var queryString = "";

            if (searchData.Skip.HasValue)
                queryString = "Skip=" + searchData.Skip.Value;

            if (searchData.Limit.HasValue)
                queryString = "Limit=" + searchData.Limit.Value;

            if (!string.IsNullOrWhiteSpace(searchData.PaymentMethodId))
                queryString = "PaymentMethodId=" + HttpUtility.UrlEncode(searchData.PaymentMethodId);

            if (!string.IsNullOrWhiteSpace(searchData.Reference))
                queryString = "Reference=" + HttpUtility.UrlEncode(searchData.Reference);

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return HttpWrapper.Call<string, CustomerList>("/customer" + queryString, Method.GET, null);
        }
    }
}
