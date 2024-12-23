using System.Collections.Generic;
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
        
        public Task<payfurl.sdk.Models.Subscriptions.Subscription> UpdateSubscriptionStatusAsync(string subscriptionId, UpdateSubscriptionStatus updateSubscriptionStatus)
        {
            return HttpWrapper.CallAsync<UpdateSubscriptionStatus, payfurl.sdk.Models.Subscriptions.Subscription>($"/subscription/{subscriptionId}/status",
                Method.PUT, updateSubscriptionStatus);
        }

        public payfurl.sdk.Models.Subscriptions.Subscription UpdateSubscriptionStatus(string subscriptionId, UpdateSubscriptionStatus updateSubscriptionStatus)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<UpdateSubscriptionStatus, payfurl.sdk.Models.Subscriptions.Subscription>($"/subscription/{subscriptionId}/status",
                Method.PUT, updateSubscriptionStatus));
        }

        public payfurl.sdk.Models.Subscriptions.Subscription UpdateSubscription(string subscriptionId, SubscriptionUpdate subscriptionUpdate)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<SubscriptionUpdate, payfurl.sdk.Models.Subscriptions.Subscription>($"/subscription/{subscriptionId}",
                Method.PUT, subscriptionUpdate));
        }
        
        public Task<payfurl.sdk.Models.Subscriptions.Subscription> UpdateSubscriptionAsync(string subscriptionId, SubscriptionUpdate subscriptionUpdate)
        {
            return HttpWrapper.CallAsync<SubscriptionUpdate, payfurl.sdk.Models.Subscriptions.Subscription>($"/subscription/{subscriptionId}",
                Method.PUT, subscriptionUpdate);
        }
        
        private static string BuildSearchQueryString(SubscriptionSearch searchData)
        {
            var queryString = new List<string>();

            if (searchData.AmountGreaterThan.HasValue)
                queryString.Add("amountGreaterThan=" +
                                HttpUtility.UrlEncode(searchData.AmountGreaterThan.Value.ToString()));

            if (searchData.AmountLessThan.HasValue)
                queryString.Add("amountLessThan=" + HttpUtility.UrlEncode(searchData.AmountLessThan.Value.ToString()));
            
            if (searchData.Skip.HasValue)
                queryString.Add("skip=" + searchData.Skip.Value);

            if (searchData.Limit.HasValue)
                queryString.Add("limit=" + searchData.Limit.Value);

            if (searchData.AddedAfter.HasValue)
                queryString.Add("addedAfter=" +
                                HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss")));

            if (searchData.AddedBefore.HasValue)
                queryString.Add("addedBefore=" +
                                HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss")));

            if (!string.IsNullOrWhiteSpace(searchData.Status))
                queryString.Add("status=" + HttpUtility.UrlEncode(searchData.Status));
            
            if (searchData.Sort != SubscriptionSearch.SortBy.None)
                queryString.Add("sortBy=" + HttpUtility.UrlEncode(searchData.Sort.ToString()));
            
            if (!string.IsNullOrWhiteSpace(searchData.Currency))
                queryString.Add("currency=" + HttpUtility.UrlEncode(searchData.Currency));

            var result = "";
            if (queryString.Count > 0)
                result = "?" + string.Join("&", queryString);
            
            return result;
        }
    }
}