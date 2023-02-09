using System;
using System.Security.Cryptography;
using System.Text;

namespace payfurl.sdk
{
    public static class WebhookTools
    {
        /// <summary>
        /// Validate that a webhook event notification came from PayFURL.Requests that fail validation
        /// should be discarded as they cannot be trusted.
        /// </summary>
        /// <param name="requestBody">HTTP request body</param>
        /// <param name="signatureHeader">Webhook signature header (X-Payfurl-Signature)</param>
        /// <param name="signatureKey">Webhook signature key (from dashboard)</param>
        /// <returns>If the request from PayFURL returns true, otherwise false</returns>
        public static bool IsFormPayfurl(string requestBody, string signatureHeader, string signatureKey)
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
