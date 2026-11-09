using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public static class WebhookTools
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            Formatting = Formatting.Indented,
            Converters = new List<JsonConverter> { new StringEnumConverter() }
        };

        /// <summary>
        /// Deserialize webhook transaction request body from PayFURL and validate that a webhook event notification came from PayFURL. Requests that fail validation
        /// will be discarded as they cannot be trusted.
        /// </summary>
        /// <param name="requestBody">HTTP request body</param>
        /// <param name="signatureHeader">Webhook signature header <code>X-Payfurl-Signature</code></param>
        /// <param name="signatureKey">Webhook signature key (from dashboard)</param>
        /// <returns>Deserialized transaction message</returns>
        /// <exception cref="ArgumentException">Thrown if signature validation fails or the signature key is empty</exception>
        public static WebhookTransaction DeserializeTransaction(string requestBody, string signatureHeader, string signatureKey)
        {
            if (!IsFormPayfurl(requestBody, signatureHeader, signatureKey))
            {
                throw new ArgumentException("Request body is not from PayFURL");
            }

            var transaction = JsonConvert.DeserializeObject<WebhookTransaction>(requestBody, JsonSettings);

            return transaction;
        }

        private static bool IsFormPayfurl(string requestBody, string signatureHeader, string signatureKey)
        {
            if (string.IsNullOrEmpty(signatureKey))
            {
                throw new ArgumentException("The webhook signature key is empty", nameof(signatureKey));
            }

            if (requestBody == null || string.IsNullOrEmpty(signatureHeader))
            {
                return false;
            }

            var requestBytes = Encoding.UTF8.GetBytes(requestBody);
            var secret = Encoding.UTF8.GetBytes(signatureKey);

            using (var hmac = new HMACSHA256(secret))
            {
                var expected = Encoding.ASCII.GetBytes(Convert.ToBase64String(hmac.ComputeHash(requestBytes)));
                var actual = Encoding.ASCII.GetBytes(signatureHeader);

                return FixedTimeEquals(expected, actual);
            }
        }

        // CryptographicOperations.FixedTimeEquals, which netstandard2.0 and .NET Framework don't have: the time taken
        // doesn't depend on where the signatures differ.
        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left.Length != right.Length)
            {
                return false;
            }

            var difference = 0;
            for (var i = 0; i < left.Length; i++)
            {
                difference |= left[i] ^ right[i];
            }

            return difference == 0;
        }
    }
}