using payfurl.sdk.Models;
using payfurl.sdk.Tools;
using System.Collections.Generic;
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

        public PaymentMethodData CreatePaymentMethodWithCard(string customerId, NewPaymentMethodCard newPaymentMethod)
        {
            return HttpWrapper.Call<NewPaymentMethodCard, PaymentMethodData>("/customer/"+ customerId + "/payment_method/card", Method.POST, newPaymentMethod);
        }

        public PaymentMethodData CreatePaymentMethodWithToken(string customerId, NewPaymentMethodToken newPaymentMethod)
        {
            return HttpWrapper.Call<NewPaymentMethodToken, PaymentMethodData>("/customer/"+ customerId + "/payment_method/token", Method.POST, newPaymentMethod);
        }

        public CustomerData Single(string customerId)
        {
            return HttpWrapper.Call<string, CustomerData>("/customer/" + customerId, Method.GET, null);
        }

        public CustomerList Search(CustomerSearch searchData)
        {
            // TODO: move into a shared class
            var queryString = "";

            if (!string.IsNullOrWhiteSpace(searchData.Reference))
                queryString = "Reference=" + HttpUtility.UrlEncode(searchData.Reference);

            if (!string.IsNullOrWhiteSpace(searchData.Email))
                queryString = "Email=" + HttpUtility.UrlEncode(searchData.Email);

            if (searchData.AddedAfter.HasValue)
                queryString = "AddedAfter=" + HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (searchData.AddedBefore.HasValue)
                queryString = "AddedBefore=" + HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (!string.IsNullOrWhiteSpace(searchData.Search))
                queryString = "Search=" + HttpUtility.UrlEncode(searchData.Search);

            if (searchData.Skip.HasValue)
                queryString = "Skip=" + searchData.Skip.Value;

            if (searchData.Limit.HasValue)
                queryString = "Limit=" + searchData.Limit.Value;

            if (!string.IsNullOrWhiteSpace(searchData.SortBy))
                queryString = "SortBy=" + HttpUtility.UrlEncode(searchData.SortBy);

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return HttpWrapper.Call<string, CustomerList>("/customer" + queryString, Method.GET, null);
        }

        public List<PaymentMethodData> GetPaymentMethods(string customerId)
        {
            customerId = HttpUtility.UrlEncode(customerId);

            return HttpWrapper.Call<string, List<PaymentMethodData>>("/customer/" + HttpUtility.UrlEncode(customerId) + "/payment_method", Method.GET, null);
        }
    }
}
