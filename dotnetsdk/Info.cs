using System.Collections.Generic;
using System.Net.Http.Headers;
using payfurl.sdk.Tools;
using System.Threading.Tasks;
using payfurl.sdk.Helpers;

namespace payfurl.sdk
{
    public class Info : IInfo
    {
        public Models.Info GetInfo()
        {
            return AsyncHelper.RunSync(() => HttpWrapper.CallAsync<string, Models.Info>("/info", Method.GET, null));
        }

        public async Task<Models.Info> GetInfoAsync()
        {
            return await HttpWrapper.CallAsync<string, Models.Info>("/info", Method.GET, null);
        }

        public Models.InfoProviders GetProvidersInfo(decimal? amount, string currency)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, Models.InfoProviders>("/info/providers?amount=" + amount + "&currency=" + currency,
                    Method.GET, null));
        }

        public async Task<Models.InfoProviders> GetProvidersInfoAsync(decimal? amount = null, string currency = null)
        {
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            if (amount != null)
                queryString.Add("amount", amount.ToString());
            if (currency != null)
                queryString.Add("currency", currency.ToString());
            
            return await HttpWrapper.CallAsync<string, Models.InfoProviders>(
                "/info/providers?" + queryString, Method.GET, null);
        }

        public Models.InfoProvider GetProviderInfo(string providerId)
        {
            var headers = new Dictionary<string, string>
            {
                {"sdk-version", "4.5.7"}
            };
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, Models.InfoProvider>("/info/providers/" + providerId, Method.GET, null, headers));
        }

        public async Task<Models.InfoProvider> GetProviderInfoAsync(string providerId)
        {
            return await HttpWrapper.CallAsync<string, Models.InfoProvider>("/info/providers/" + providerId, Method.GET, null);
        }

        public Models.InfoProvider GetProviderTokenInfo(string token, decimal? amount = null, string currency = null)
        {
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            if (amount != null)
                queryString.Add("amount", amount.ToString());
            if (currency != null)
                queryString.Add("currency", currency.ToString());

            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, Models.InfoProvider>(
                    "/info/providers/token/" + token + "?" + queryString, Method.GET, null));
        }

        public async Task<Models.InfoProvider> GetProviderTokenInfoAsync(string token, decimal? amount, string currency)
        {
            return await HttpWrapper.CallAsync<string, Models.InfoProvider>(
                "/info/providers/token/" + token + "?amount=" + amount + "&currency=" + currency, Method.GET, null);
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
    }
}