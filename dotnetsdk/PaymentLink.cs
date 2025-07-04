using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models.PaymentLink;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class PaymentLink : IPaymentLink
    {
        public PaymentLinkData Create(CreatePaymentLink paymentLink)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<CreatePaymentLink, PaymentLinkData>("/payment_link", Method.POST, paymentLink));
        }
        
        public async Task<PaymentLinkData> CreateAsync(CreatePaymentLink paymentLink)
        {
            return await HttpWrapper.CallAsync<CreatePaymentLink, PaymentLinkData>("/payment_link", Method.POST, paymentLink);
        }
        
        public PaymentLinkData Single(string paymentLinkId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, PaymentLinkData>("/payment_link/" + paymentLinkId, Method.GET, null));
        }

        public async Task<PaymentLinkData> SingleAsync(string paymentLinkId)
        {
            return await HttpWrapper.CallAsync<string, PaymentLinkData>("/payment_link/" + paymentLinkId, Method.GET, null);
        }

        public SearchPaymentLinkResult Search(SearchPaymentLink searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, SearchPaymentLinkResult>("/payment_link" + queryString, Method.GET, null));
        }

        public async Task<SearchPaymentLinkResult> SearchAsync(SearchPaymentLink searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return await HttpWrapper.CallAsync<string, SearchPaymentLinkResult>("/payment_link" + queryString, Method.GET, null);
        }

        private static string BuildSearchQueryString(SearchPaymentLink searchData)
        {
            // TODO: move into a shared class to handle formatting
            var queryString = new List<string>();

            if (searchData.AddedAfter.HasValue)
                queryString.Add("AddedAfter=" +
                                HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss")));

            if (searchData.AddedBefore.HasValue)
                queryString.Add("AddedBefore=" +
                                HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss")));

            if (searchData.Sort.HasValue && searchData.Sort != SearchPaymentLink.SortBy.None)
                queryString.Add("SortBy=" + searchData.Sort);

            if (searchData.Skip.HasValue)
                queryString.Add("Skip=" + searchData.Skip.Value);

            if (searchData.Limit.HasValue)
                queryString.Add("Limit=" + searchData.Limit.Value);

            var result = "";
            if (queryString.Count > 0)
                result = "?" + string.Join("&", queryString);
            
            return result;
        }
    }
}