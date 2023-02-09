using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;
using System.Web;
using payfurl.sdk.Helpers;

namespace payfurl.sdk
{
    public class Charge : ICharge
    {
        public ChargeData CreateWithCard(NewChargeCard newCharge)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewChargeCard, ChargeData>("/charge/card", Method.POST, newCharge));
        }

        public async Task<ChargeData> CreateWithCardAsync(NewChargeCard newCharge)
        {
            return await HttpWrapper.CallAsync<NewChargeCard, ChargeData>("/charge/card", Method.POST, newCharge);
        }

        public ChargeData CreateWithCardLeastCost(NewChargeCardLeastCost newCharge)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewChargeCardLeastCost, ChargeData>("/charge/card/least_cost", Method.POST,
                    newCharge));
        }

        public async Task<ChargeData> CreateWithCardLeastCostAsync(NewChargeCardLeastCost newCharge)
        {
            return await HttpWrapper.CallAsync<NewChargeCardLeastCost, ChargeData>("/charge/card/least_cost",
                Method.POST, newCharge);
        }

        public ChargeData CreateWithPaymentMethod(NewChargePaymentMethod newCharge)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewChargePaymentMethod, ChargeData>("/charge/payment_method", Method.POST,
                    newCharge));
        }

        public async Task<ChargeData> CreateWithPaymentMethodAsync(NewChargePaymentMethod newCharge)
        {
            return await HttpWrapper.CallAsync<NewChargePaymentMethod, ChargeData>("/charge/payment_method",
                Method.POST, newCharge);
        }

        public ChargeData CreateWithCustomer(NewChargeCustomer newCharge)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewChargeCustomer, ChargeData>("/charge/customer", Method.POST, newCharge));
        }

        public async Task<ChargeData> CreateWithCustomerAsync(NewChargeCustomer newCharge)
        {
            return await HttpWrapper.CallAsync<NewChargeCustomer, ChargeData>("/charge/customer", Method.POST,
                newCharge);
        }

        public ChargeData CreateWithToken(NewChargeToken newCharge)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewChargeToken, ChargeData>("/charge/token", Method.POST, newCharge));
        }

        public async Task<ChargeData> CreateWithTokenAsync(NewChargeToken newCharge)
        {
            return await HttpWrapper.CallAsync<NewChargeToken, ChargeData>("/charge/token", Method.POST, newCharge);
        }

        public ChargeData Refund(NewRefund newCharge)
        {
            var queryString = BuildRefundQueryString(newCharge);

            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, ChargeData>("/charge/" + newCharge.ChargeId + queryString, Method.DELETE,
                    null));
        }

        public async Task<ChargeData> RefundAsync(NewRefund newCharge)
        {
            var queryString = BuildRefundQueryString(newCharge);

            return await HttpWrapper.CallAsync<string, ChargeData>("/charge/" + newCharge.ChargeId + queryString,
                Method.DELETE, null);
        }

        public ChargeData Capture(string chargeId, NewChargeCapture chargeCaptureData)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewChargeCapture, ChargeData>($"/charge/{chargeId}", Method.POST,
                    chargeCaptureData));
        }

        public async Task<ChargeData> CaptureAsync(string chargeId, NewChargeCapture chargeCaptureData)
        {
            return await HttpWrapper.CallAsync<NewChargeCapture, ChargeData>($"/charge/{chargeId}", Method.POST,
                chargeCaptureData);
        }

        public ChargeData Void(string chargeId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, ChargeData>($"/charge/{chargeId}", Method.DELETE, null));
        }

        public async Task<ChargeData> VoidAsync(string chargeId)
        {
            return await HttpWrapper.CallAsync<string, ChargeData>($"/charge/{chargeId}", Method.DELETE, null);
        }

        public ChargeData Single(string chargeId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, ChargeData>("/charge/" + chargeId, Method.GET, null));
        }

        public async Task<ChargeData> SingleAsync(string chargeId)
        {
            return await HttpWrapper.CallAsync<string, ChargeData>("/charge/" + chargeId, Method.GET, null);
        }

        public ChargeList Search(ChargeSearch searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, ChargeList>("/charge" + queryString, Method.GET, null));
        }

        public async Task<ChargeList> SearchAsync(ChargeSearch searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return await HttpWrapper.CallAsync<string, ChargeList>("/charge" + queryString, Method.GET, null);
        }

        /// <summary>
        /// Validate that a webhook event notification came from PayFURL.Requests that fail validation
        /// should be discarded as they cannot be trusted.
        /// </summary>
        /// <param name="request">HTTP Request</param>
        /// <param name="webhookSignatureKey">Webhook signature key (from dashboard)</param>
        /// <returns>If the request from PayFURL returns true, otherwise false</returns>
        public static async Task<bool> IsFromPayFurl(HttpRequestMessage request, string webhookSignatureKey)
        {
            var signature = request.Headers.GetValues("X-Payfurl-Signature").FirstOrDefault();
            var requestBody = await request.Content.ReadAsStringAsync();

            return IsValidWebhook(requestBody, signature, webhookSignatureKey);
        }

        private static string BuildRefundQueryString(NewRefund newCharge)
        {
            var queryString = "";

            if (newCharge.RefundAmount.HasValue)
            {
                queryString = "?amount=" + newCharge.RefundAmount.Value;
            }

            return queryString;
        }

        private static string BuildSearchQueryString(ChargeSearch searchData)
        {
            // TODO: move into a shared class to handle formatting
            var queryString = "";

            if (searchData.Skip.HasValue)
                queryString = "Skip=" + searchData.Skip.Value;

            if (searchData.Limit.HasValue)
                queryString = "Limit=" + searchData.Limit.Value;

            if (!string.IsNullOrWhiteSpace(searchData.Reference))
                queryString = "Reference=" + HttpUtility.UrlEncode(searchData.Reference);

            if (!string.IsNullOrWhiteSpace(searchData.PaymentMethodId))
                queryString = "PaymentMethodId=" + HttpUtility.UrlEncode(searchData.PaymentMethodId);

            if (searchData.AmountGreaterThan.HasValue)
                queryString = "AmountGreaterThan=" +
                              HttpUtility.UrlEncode(searchData.AmountGreaterThan.Value.ToString());

            if (searchData.AmountLessThan.HasValue)
                queryString = "AmountLessThan=" + HttpUtility.UrlEncode(searchData.AmountLessThan.Value.ToString());

            if (!string.IsNullOrWhiteSpace(searchData.CustomerId))
                queryString = "CustomerId=" + HttpUtility.UrlEncode(searchData.CustomerId);

            if (!string.IsNullOrWhiteSpace(searchData.Status))
                queryString = "Status=" + HttpUtility.UrlEncode(searchData.Status);

            if (searchData.AddedAfter.HasValue)
                queryString = "AddedAfter=" +
                              HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (searchData.AddedBefore.HasValue)
                queryString = "AddedBefore=" +
                              HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (!string.IsNullOrWhiteSpace(searchData.SortBy))
                queryString = "SortBy=" + searchData.SortBy;

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return queryString;
        }

        private static bool IsValidWebhook(string requestBody, string signatureHeader, string signatureKey)
        {
            var requestBytes = Encoding.UTF8.GetBytes(requestBody);
            var secret = Encoding.UTF8.GetBytes(signatureKey);

            using (var hmac = new HMACSHA256(secret))
            {
                var hash = hmac.ComputeHash(requestBytes);
                var hashString = Convert.ToBase64String(hash);

                return hashString.Equals(signatureHeader);
            }
        }
    }
}