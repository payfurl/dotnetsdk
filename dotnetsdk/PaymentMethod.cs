using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class PaymentMethod : IPaymentMethod
    {
        public PaymentMethodData CreatePaymentMethodWithVault(NewPaymentMethodVault newPaymentMethodVault)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewPaymentMethodVault, PaymentMethodData>("/payment_method/vault", Method.POST,
                    newPaymentMethodVault));
        }

        public async Task<PaymentMethodData> CreatePaymentMethodWithVaultAsync(
            NewPaymentMethodVault newPaymentMethodVault)
        {
            return await HttpWrapper.CallAsync<NewPaymentMethodVault, PaymentMethodData>("/payment_method/vault",
                Method.POST, newPaymentMethodVault);
        }

        public PaymentMethodData CreatePaymentMethodWithCard(NewPaymentMethodCard newPaymentMethodCard)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewPaymentMethodCard, PaymentMethodData>("/payment_method/card", Method.POST,
                    newPaymentMethodCard));
        }

        public async Task<PaymentMethodData> CreatePaymentMethodWithCardAsync(
            NewPaymentMethodCard newNewPaymentMethodCard)
        {
            return await HttpWrapper.CallAsync<NewPaymentMethodCard, PaymentMethodData>("/payment_method/card",
                Method.POST, newNewPaymentMethodCard);
        }
        
        public PaymentMethodData CreatePaymentMethodWithSingleUseToken(NewPaymentMethodSingleUseToken newPaymentMethodSingleUseToken)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewPaymentMethodSingleUseToken, PaymentMethodData>("/payment_method/provider_single_use_token", Method.POST,
                    newPaymentMethodSingleUseToken));
        }

        public async Task<PaymentMethodData> CreatePaymentMethodWithSingleUseTokenAsync(
            NewPaymentMethodSingleUseToken newPaymentMethodSingleUseToken)
        {
            return await HttpWrapper.CallAsync<NewPaymentMethodSingleUseToken, PaymentMethodData>("/payment_method/provider_single_use_token",
                Method.POST, newPaymentMethodSingleUseToken);
        }

        public PaymentMethodData CreateWithProviderToken(NewProviderToken providerToken)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewProviderToken, PaymentMethodData>("/payment_method/provider_token", Method.POST,
                    providerToken));
        }

        public async Task<PaymentMethodData> CreateWithProviderTokenAsync(NewProviderToken providerToken)
        {
            return await HttpWrapper.CallAsync<NewProviderToken, PaymentMethodData>("/payment_method/provider_token",
                Method.POST, providerToken);
        }

        public PaymentMethodData CreatePaymentMethodWithPayto(NewPayToAgreement newNewPaymentMethodPayTo)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewPayToAgreement, PaymentMethodData>("/payment_method/payto", Method.POST,
                    newNewPaymentMethodPayTo));
        }

        public async Task<PaymentMethodData> CreatePaymentMethodWithPaytoAsync(NewPayToAgreement newNewPaymentMethodPayTo)
        {
            return await HttpWrapper.CallAsync<NewPayToAgreement, PaymentMethodData>("/payment_method/payto", Method.POST,
                    newNewPaymentMethodPayTo);
        }

        public PaymentMethodData Single(string paymentMethodId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, PaymentMethodData>("/payment_method/" + paymentMethodId, Method.GET,
                    null));
        }

        public async Task<PaymentMethodData> SingleAsync(string paymentMethodId)
        {
            return await HttpWrapper.CallAsync<string, PaymentMethodData>("/payment_method/" + paymentMethodId,
                Method.GET, null);
        }

        public PaymentMethodList Search(PaymentMethodSearch searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, PaymentMethodList>("/payment_method" + queryString, Method.GET, null));
        }

        public async Task<PaymentMethodList> SearchAsync(PaymentMethodSearch searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return await HttpWrapper.CallAsync<string, PaymentMethodList>("/payment_method" + queryString, Method.GET,
                null);
        }
        
        public PaymentMethodData CreatePaymentMethodWithBankAccount(NewPaymentMethodBankPayment newPaymentMethodCard)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewPaymentMethodBankPayment, PaymentMethodData>("/payment_method/bank_account", Method.POST,
                    newPaymentMethodCard));
        }

        public async Task<PaymentMethodData> CreatePaymentMethodWithBankAccountAsync(
            NewPaymentMethodBankPayment newNewPaymentMethodCard)
        {
            return await HttpWrapper.CallAsync<NewPaymentMethodBankPayment, PaymentMethodData>("/payment_method/bank_account",
                Method.POST, newNewPaymentMethodCard);
        }

        private static string BuildSearchQueryString(PaymentMethodSearch searchData)
        {
            // TODO: move into a shared class
            var queryString = new List<string>();

            if (searchData.Skip.HasValue)
                queryString.Add("Skip=" + searchData.Skip.Value);

            if (searchData.Limit.HasValue)
                queryString.Add("Limit=" + searchData.Limit.Value);

            if (!string.IsNullOrWhiteSpace(searchData.ProviderId))
                queryString.Add("ProviderId=" + HttpUtility.UrlEncode(searchData.ProviderId));

            if (!string.IsNullOrWhiteSpace(searchData.CustomerId))
                queryString.Add("CustomerId=" + HttpUtility.UrlEncode(searchData.CustomerId));

            if (!string.IsNullOrWhiteSpace(searchData.Search))
                queryString.Add("Search=" + HttpUtility.UrlEncode(searchData.Search));

            if (searchData.AddedAfter.HasValue)
                queryString.Add("AddedAfter=" +
                                HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss")));

            if (searchData.AddedBefore.HasValue)
                queryString.Add("AddedBefore=" +
                                HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss")));

            if (!string.IsNullOrWhiteSpace(searchData.PaymentType))
                queryString.Add("PaymentType=" + HttpUtility.UrlEncode(searchData.PaymentType));
            
            if (!string.IsNullOrWhiteSpace(searchData.CardType))
                queryString.Add("CardType=" + HttpUtility.UrlEncode(searchData.CardType));

            if (!string.IsNullOrWhiteSpace(searchData.SortBy))
                queryString.Add("SortBy=" + HttpUtility.UrlEncode(searchData.SortBy));

            var result = "";
            if (queryString.Count > 0)
                result = "?" + string.Join("&", queryString);
            
            return result;
        }

        public PaymentMethodData Delete(string paymentMethodId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, PaymentMethodData>("/payment_method/" + paymentMethodId, Method.DELETE, null));
        }

        public async Task<PaymentMethodData> DeleteAsync(string paymentMethodId)
        {
            return await HttpWrapper.CallAsync<string, PaymentMethodData>("/payment_method/" + paymentMethodId, Method.DELETE, null);
        }

        public PaymentMethodData UpdatePaymentMethod(string paymentMethodId, UpdatePaymentMethod updatePaymentMethod)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<UpdatePaymentMethod, PaymentMethodData>("/payment_method/" + paymentMethodId, Method.PUT, updatePaymentMethod));
        }

        public async Task<PaymentMethodData> UpdatePaymentMethodAsync(string paymentMethodId,
            UpdatePaymentMethod updatePaymentMethod)
        {
            return await HttpWrapper.CallAsync<UpdatePaymentMethod, PaymentMethodData>("/payment_method/" + paymentMethodId, Method.PUT, updatePaymentMethod);
        }

        public PaymentMethodData CreateWithToken(NewPaymentMethodToken token)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewPaymentMethodToken, PaymentMethodData>("/payment_method/token", Method.POST, token));
        }

        public async Task<PaymentMethodData> CreateWithTokenAsync(NewPaymentMethodToken token)
        {
            return await HttpWrapper.CallAsync<NewPaymentMethodToken, PaymentMethodData>("/payment_method/token", Method.POST, token);
        }
    }
}
