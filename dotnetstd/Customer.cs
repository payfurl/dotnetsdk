using payfurl.sdk.Models;
using payfurl.sdk.Tools;
using System.Web;

namespace payfurl.sdk
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

        public CustomerData Single(string customerId)
        {
            return HttpWrapper.Call<string, CustomerData>("/customer/" + customerId, Method.GET, null);
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

            if (!string.IsNullOrWhiteSpace(searchData.CustomerId))
                queryString = "CustomerId=" + HttpUtility.UrlEncode(searchData.CustomerId);

            if (!string.IsNullOrWhiteSpace(searchData.Email))
                queryString = "Email=" + HttpUtility.UrlEncode(searchData.Email);

            if (!string.IsNullOrWhiteSpace(searchData.Search))
                queryString = "Search=" + HttpUtility.UrlEncode(searchData.Search);

            if (searchData.AddedAfter.HasValue)
                queryString = "AddedAfter=" + HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (searchData.AddedBefore.HasValue)
                queryString = "AddedBefore=" + HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return HttpWrapper.Call<string, CustomerList>("/customer" + queryString, Method.GET, null);
        }
    }
}
