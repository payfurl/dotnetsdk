using System.Threading.Tasks;
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
            return HttpWrapper.CallAsync<SubscriptionSearch, SubscriptionList>("/subscription",
                Method.POST, search);
        }

        public SubscriptionList SearchSubscription(SubscriptionSearch search)
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<SubscriptionSearch, SubscriptionList>("/subscription",
                Method.POST, search));
        }
    }
}