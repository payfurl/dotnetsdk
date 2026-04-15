using System.Collections.Generic;
using System.Globalization;
using payfurl.sdk.Tools;
using System.Threading.Tasks;
using payfurl.sdk.Helpers;
using System.Web;

namespace payfurl.sdk
{
    public class Info : IInfo
    {
        private const string SdkVersionHeaderValue = "4.5.7";

        public Models.Info GetInfo()
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<string, Models.Info>("/info", Method.GET, null));
        }

        public async Task<Models.Info> GetInfoAsync()
        {
            return await HttpWrapper.CallAsync<string, Models.Info>("/info", Method.GET, null);
        }

        public Models.InfoProviders GetProvidersInfo(decimal? amount = null, string currency = null,
            string cardProviderId = null)
        {
            return AsyncHelper.RunSync(() => GetProvidersInfoAsync(amount, currency, cardProviderId));
        }

        public async Task<Models.InfoProviders> GetProvidersInfoAsync(decimal? amount = null, string currency = null,
            string cardProviderId = null)
        {
            return await HttpWrapper.CallAsync<string, Models.InfoProviders>(
                "/info/providers" + BuildInfoQueryString(amount, currency, cardProviderId), Method.GET, null,
                BuildProviderHeaders());
        }

        public Models.InfoProvider GetProviderInfo(string providerId, decimal? amount = null, string currency = null)
        {
            return AsyncHelper.RunSync(() => GetProviderInfoAsync(providerId, amount, currency));
        }

        public async Task<Models.InfoProvider> GetProviderInfoAsync(string providerId, decimal? amount = null,
            string currency = null)
        {
            return await HttpWrapper.CallAsync<string, Models.InfoProvider>(
                "/info/providers/" + providerId + BuildInfoQueryString(amount, currency), Method.GET, null,
                BuildProviderHeaders());
        }

        public Models.InfoProvider GetProviderTokenInfo(string token, decimal? amount = null, string currency = null)
        {
            return AsyncHelper.RunSync(() => GetProviderTokenInfoAsync(token, amount, currency));
        }

        public async Task<Models.InfoProvider> GetProviderTokenInfoAsync(string token, decimal? amount = null,
            string currency = null)
        {
            return await HttpWrapper.CallAsync<string, Models.InfoProvider>(
                "/info/providers/token/" + token + BuildInfoQueryString(amount, currency), Method.GET, null,
                BuildProviderHeaders());
        }

        public Models.ProviderData GetDefaultFallbackProvider()
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, Models.ProviderData>("/info/default_fallback_provider", Method.GET, null));
        }

        public async Task<Models.ProviderData> GetDefaultFallbackProviderAsync()
        {
            return await HttpWrapper.CallAsync<string, Models.ProviderData>("/info/default_fallback_provider", Method.GET, null);
        }

        private static string BuildInfoQueryString(decimal? amount = null, string currency = null,
            string cardProviderId = null)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            if (amount.HasValue)
                queryString.Add("amount", amount.Value.ToString(CultureInfo.InvariantCulture));
            if (!string.IsNullOrWhiteSpace(currency))
                queryString.Add("currency", currency);
            if (!string.IsNullOrWhiteSpace(cardProviderId))
                queryString.Add("cardProviderId", cardProviderId);

            var query = queryString.ToString();
            return string.IsNullOrEmpty(query) ? "" : "?" + query;
        }

        private static Dictionary<string, string> BuildProviderHeaders()
        {
            return new Dictionary<string, string>
            {
                { "sdk-version", SdkVersionHeaderValue }
            };
        }
    }
}
