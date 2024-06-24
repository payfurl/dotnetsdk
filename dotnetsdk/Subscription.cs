using System.Threading.Tasks;
using System.Web;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models.Subscriptions;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class Subscription : ISubscription
    {
        public Task<payfurl.sdk.Models.Subscriptions.Subscription> CreateSubscriptionAsync(SubscriptionCreate subscriptionCreate)
        {
            return HttpWrapper.CallAsync<SubscriptionCreate, payfurl.sdk.Models.Subscriptions.Subscription>("/subscription/payment_method",
                Method.POST, subscriptionCreate);
        }

        public payfurl.sdk.Models.Subscriptions.Subscription CreateSubscription(SubscriptionCreate subscriptionCreate)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<SubscriptionCreate, payfurl.sdk.Models.Subscriptions.Subscription>("/subscription/payment_method",
                Method.POST, subscriptionCreate));
        }

        public Task<payfurl.sdk.Models.Subscriptions.Subscription> GetSubscriptionAsync(string subscriptionId)
        {
            return HttpWrapper.CallAsync<string, payfurl.sdk.Models.Subscriptions.Subscription>($"/subscription/{subscriptionId}",
                Method.GET, null);
        }

        public payfurl.sdk.Models.Subscriptions.Subscription GetSubscription(string subscriptionId)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<string, payfurl.sdk.Models.Subscriptions.Subscription>($"/subscription/{subscriptionId}",
                Method.GET, null));
        }

        public Task<payfurl.sdk.Models.Subscriptions.Subscription> DeleteSubscriptionAsync(string subscriptionId)
        {
            return HttpWrapper.CallAsync<string, payfurl.sdk.Models.Subscriptions.Subscription>($"/subscription/{subscriptionId}",
                Method.DELETE, null);
        }

        public payfurl.sdk.Models.Subscriptions.Subscription DeleteSubscription(string subscriptionId)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<string, payfurl.sdk.Models.Subscriptions.Subscription>($"/subscription/{subscriptionId}",
                Method.DELETE, null));
        }

        public Task<SubscriptionList> SearchSubscriptionAsync(SubscriptionSearch search)
        {
            var queryString = BuildSearchQueryString(search);
            
            return HttpWrapper.CallAsync<SubscriptionSearch, SubscriptionList>("/subscription" + queryString,
                Method.GET, search);
        }

        public SubscriptionList SearchSubscription(SubscriptionSearch search)
        {
            var queryString = BuildSearchQueryString(search);
            
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<SubscriptionSearch, SubscriptionList>("/subscription" + queryString,
                Method.GET, search));
        }
        
        private static string BuildSearchQueryString(SubscriptionSearch searchData)
        {
            var queryString = "";

            if (searchData.AmountGreaterThan.HasValue)
                queryString = "amountGreaterThan=" +
                              HttpUtility.UrlEncode(searchData.AmountGreaterThan.Value.ToString());

            if (searchData.AmountLessThan.HasValue)
                queryString = "amountLessThan=" + HttpUtility.UrlEncode(searchData.AmountLessThan.Value.ToString());
            
            if (searchData.Skip.HasValue)
                queryString = "skip=" + searchData.Skip.Value;

            if (searchData.Limit.HasValue)
                queryString = "limit=" + searchData.Limit.Value;

            if (searchData.AddedAfter.HasValue)
                queryString = "addedAfter=" +
                              HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (searchData.AddedBefore.HasValue)
                queryString = "addedBefore=" +
                              HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (!string.IsNullOrWhiteSpace(searchData.Status))
                queryString = "status=" + HttpUtility.UrlEncode(searchData.Status);
            
            if (!string.IsNullOrWhiteSpace(searchData.Status))
                queryString = "sortBy=" + HttpUtility.UrlEncode(searchData.SortBy);
            
            if (!string.IsNullOrWhiteSpace(searchData.Currency))
                queryString = "currency=" + HttpUtility.UrlEncode(searchData.Currency);

            
            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return queryString;
        }
    }
}