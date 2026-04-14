using System.Threading.Tasks;

namespace payfurl.sdk
{
    public interface IInfo
    {
        Models.Info GetInfo();
        Task<Models.Info> GetInfoAsync();
        Models.InfoProviders GetProvidersInfo(decimal? amount = null, string currency = null,
            string cardProviderId = null);
        Task<Models.InfoProviders> GetProvidersInfoAsync(decimal? amount = null, string currency = null,
            string cardProviderId = null);
        Models.InfoProvider GetProviderInfo(string providerId, decimal? amount = null, string currency = null);
        Task<Models.InfoProvider> GetProviderInfoAsync(string providerId, decimal? amount = null,
            string currency = null);
        Models.InfoProvider GetProviderTokenInfo(string token, decimal? amount = null, string currency = null);
        Task<Models.InfoProvider> GetProviderTokenInfoAsync(string token, decimal? amount = null,
            string currency = null);
        Models.ProviderData GetDefaultFallbackProvider();
        Task<Models.ProviderData> GetDefaultFallbackProviderAsync();
    }
}
