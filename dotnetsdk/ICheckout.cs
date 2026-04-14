using System.Threading.Tasks;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface ICheckout
    {
        Models.Checkout Create(NewCheckout checkout);
        Task<Models.Checkout> CreateAsync(NewCheckout checkout);
        ExternalCheckoutData Get(string checkoutId);
        Task<ExternalCheckoutData> GetAsync(string checkoutId);
        ExternalCheckoutData GetByExternalId(string externalId);
        Task<ExternalCheckoutData> GetByExternalIdAsync(string externalId);
    }
}
