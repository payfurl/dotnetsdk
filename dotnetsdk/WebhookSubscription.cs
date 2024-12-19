using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class WebhookSubscription : IWebhookSubscription
    {
        public Task<WebhookSubscriptionData> CreateWebhookSubscriptionAsync(NewWebhookSubscription newWebhookSubscription)
        {
            return HttpWrapper.CallAsync<NewWebhookSubscription, WebhookSubscriptionData>("/webhook/subscription",
                Method.POST, newWebhookSubscription);
        }

        public WebhookSubscriptionData CreateWebhookSubscription(NewWebhookSubscription newWebhookSubscription)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<NewWebhookSubscription, WebhookSubscriptionData>("/webhook/subscription",
                Method.POST, newWebhookSubscription));
        }

        public Task<WebhookSubscriptionData> GetWebhookSubscriptionAsync(string webhookSubscriptionId)
        {
            return HttpWrapper.CallAsync<string, WebhookSubscriptionData>($"/webhook/subscription/{webhookSubscriptionId}",
                Method.GET, null);
        }

        public WebhookSubscriptionData GetWebhookSubscription(string webhookSubscriptionId)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<string, WebhookSubscriptionData>($"/webhook/subscription/{webhookSubscriptionId}",
                Method.GET, null));
        }

        public Task<WebhookSubscriptionData> DeleteWebhookSubscriptionAsync(string webhookSubscriptionId)
        {
            return HttpWrapper.CallAsync<string, WebhookSubscriptionData>($"/webhook/subscription/{webhookSubscriptionId}",
                Method.DELETE, null);
        }

        public WebhookSubscriptionData DeleteWebhookSubscription(string webhookSubscriptionId)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<string, WebhookSubscriptionData>($"/webhook/subscription/{webhookSubscriptionId}",
                Method.DELETE, null));
        }

        public Task<WebhookSubscriptionSearchResults> SearchWebhookSubscriptionAsync(WebhookSubscriptionSearch search)
        {
            var queryString = BuildSearchQueryString(search);
            
            return HttpWrapper.CallAsync<WebhookSubscriptionSearch, WebhookSubscriptionSearchResults>("/webhook/subscription" + queryString,
                Method.GET, search);
        }

        public WebhookSubscriptionSearchResults SearchWebhookSubscription(WebhookSubscriptionSearch search)
        {
            var queryString = BuildSearchQueryString(search);
            
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<WebhookSubscriptionSearch, WebhookSubscriptionSearchResults>("/webhook/subscription" + queryString,
                Method.GET, search));
        }
        
        private static string BuildSearchQueryString(WebhookSubscriptionSearch searchData)
        {
            var queryString = new List<string>();

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

            if (!string.IsNullOrWhiteSpace(searchData.Type))
                queryString.Add("type=" + HttpUtility.UrlEncode(searchData.Type));
            
            if (!string.IsNullOrWhiteSpace(searchData.Id))
                queryString.Add("id=" + HttpUtility.UrlEncode(searchData.Id));

            if (searchData.Sort != WebhookSubscriptionSearch.SortBy.None)
                queryString.Add("sort=" + HttpUtility.UrlEncode(searchData.Sort.ToString()));

            var result = "";
            if (queryString.Count > 0)
                result = "?" + string.Join("&", queryString);
            
            return result;
        }
    }
}