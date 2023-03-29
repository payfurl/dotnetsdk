﻿using System.Threading.Tasks;
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

        private static string BuildSearchQueryString(PaymentMethodSearch searchData)
        {
            // TODO: move into a shared class
            var queryString = "";

            if (searchData.Skip.HasValue)
                queryString = "Skip=" + searchData.Skip.Value;

            if (searchData.Limit.HasValue)
                queryString = "Limit=" + searchData.Limit.Value;

            if (!string.IsNullOrWhiteSpace(searchData.ProviderId))
                queryString = "ProviderId=" + HttpUtility.UrlEncode(searchData.ProviderId);

            if (!string.IsNullOrWhiteSpace(searchData.CustomerId))
                queryString = "CustomerId=" + HttpUtility.UrlEncode(searchData.CustomerId);

            if (!string.IsNullOrWhiteSpace(searchData.Search))
                queryString = "Search=" + HttpUtility.UrlEncode(searchData.Search);

            if (searchData.AddedAfter.HasValue)
                queryString = "AddedAfter=" +
                              HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (searchData.AddedBefore.HasValue)
                queryString = "AddedBefore=" +
                              HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (!string.IsNullOrWhiteSpace(searchData.PaymentType))
                queryString = "PaymentType=" + HttpUtility.UrlEncode(searchData.PaymentType);

            if (!string.IsNullOrWhiteSpace(searchData.SortBy))
                queryString = "SortBy=" + HttpUtility.UrlEncode(searchData.SortBy);

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return queryString;
        }
    }
}
