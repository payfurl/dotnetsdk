using System.Threading.Tasks;
using payfurl.sdk.Models.Subscriptions;

namespace payfurl.sdk
{
    public interface ISubscription
    {
        Task<payfurl.sdk.Models.Subscriptions.Subscription> CreateSubscriptionAsync(SubscriptionCreate subscriptionCreate);
        payfurl.sdk.Models.Subscriptions.Subscription CreateSubscription(SubscriptionCreate subscriptionCreate);
        Task<payfurl.sdk.Models.Subscriptions.Subscription> GetSubscriptionAsync(string subscriptionId);
        payfurl.sdk.Models.Subscriptions.Subscription GetSubscription(string subscriptionId);
        Task<payfurl.sdk.Models.Subscriptions.Subscription> DeleteSubscriptionAsync(string subscriptionId);
        payfurl.sdk.Models.Subscriptions.Subscription DeleteSubscription(string subscriptionId);
        Task<SubscriptionList> SearchSubscriptionAsync(SubscriptionSearch search);
        SubscriptionList SearchSubscription(SubscriptionSearch search);
    }
}