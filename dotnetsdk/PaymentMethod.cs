using System.Web;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class PaymentMethod : IPaymentMethod
    {
        public Checkout Checkout(NewCheckout newCheckout)
        {
            return HttpWrapper.Call<NewCheckout, Checkout>("/payment_method/checkout", Method.POST, newCheckout);
        }

        public PaymentMethodList Search(PaymentMethodSearch searchData)
        {
            // TODO: move into a shared class
            var queryString = "";

            if (searchData.AddedAfter.HasValue)
                queryString = "AddedAfter=" + HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (searchData.AddedBefore.HasValue)
                queryString = "AddedBefore=" + HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (!string.IsNullOrWhiteSpace(searchData.ProviderId))
                queryString = "ProviderId=" + HttpUtility.UrlEncode(searchData.ProviderId);

            if (!string.IsNullOrWhiteSpace(searchData.CustomerId))
                queryString = "CustomerId=" + HttpUtility.UrlEncode(searchData.CustomerId);

            if (searchData.Skip.HasValue)
                queryString = "Skip=" + searchData.Skip.Value;

            if (searchData.Limit.HasValue)
                queryString = "Limit=" + searchData.Limit.Value;

            if (!string.IsNullOrWhiteSpace(searchData.PaymentType))
                queryString = "PaymentType=" + HttpUtility.UrlEncode(searchData.PaymentType);

            if (!string.IsNullOrWhiteSpace(searchData.CustomerId))
                queryString = "CustomerId=" + HttpUtility.UrlEncode(searchData.CustomerId);

            if (!string.IsNullOrWhiteSpace(searchData.Search))
                queryString = "Search=" + HttpUtility.UrlEncode(searchData.Search);

            if (!string.IsNullOrWhiteSpace(searchData.SortBy))
                queryString = "SortBy=" + HttpUtility.UrlEncode(searchData.SortBy);

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return HttpWrapper.Call<string, PaymentMethodList>("/payment_method" + queryString, Method.GET, null);
        }

        public PaymentMethodData Single(string paymentMethodId)
        {
            return HttpWrapper.Call<string, PaymentMethodData>("/payment_method/" + HttpUtility.UrlEncode(paymentMethodId), Method.GET, null);
        }
    }
}
