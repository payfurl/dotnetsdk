using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace payfurl.sdk
{
    public static class WebhookTools
    {
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
