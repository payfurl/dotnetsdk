using System.Threading.Tasks;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class Checkout : ICheckout
    {
        public Models.Checkout Create(NewCheckout checkout)
        {
            return AsyncHelper.RunSync(() => CreateAsync(checkout));
        }

        public async Task<Models.Checkout> CreateAsync(NewCheckout checkout)
        {
            return await HttpWrapper.CallAsync<NewCheckout, Models.Checkout>(
                "/payment_method/checkout", Method.POST, checkout);
        }

        public ExternalCheckoutData Get(string checkoutId)
        {
            return AsyncHelper.RunSync(() => GetAsync(checkoutId));
        }

        public async Task<ExternalCheckoutData> GetAsync(string checkoutId)
        {
            return await HttpWrapper.CallAsync<string, ExternalCheckoutData>(
                "/checkout/" + checkoutId, Method.GET, null);
        }

        public ExternalCheckoutData GetByExternalId(string externalId)
        {
            return AsyncHelper.RunSync(() => GetByExternalIdAsync(externalId));
        }

        public async Task<ExternalCheckoutData> GetByExternalIdAsync(string externalId)
        {
            return await HttpWrapper.CallAsync<string, ExternalCheckoutData>(
                "/checkout/external/" + externalId, Method.GET, null);
        }
    }
}
