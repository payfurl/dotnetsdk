using payfurl.sdk.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace payfurl.sdk.Tools
{
    public enum Method
    {
        GET,
        POST,
        DELETE,
        PUT
    }

    public static class HttpWrapper
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        // PooledConnectionLifetime prevents DNS caching but is not supported with current .NET versions
        private static readonly HttpClient HttpClient = new HttpClient(/*new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(1)
        }*/)
        {
            Timeout = TimeSpan.FromMilliseconds(Config.TimeoutMilliseconds)
        };

        private const string MediaType = "application/json";

        static HttpWrapper()
        {
            // Enforce min TLS 1.2
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
        }

        public static async Task<TResult> CallAsync<TInput, TResult>(string endpoint, Method httpMethod, TInput body)
        {
            var httpRequest = PrepareHttpRequestMessage<TInput>(endpoint, httpMethod, body);

            var responseString = string.Empty;
            var response = new HttpResponseMessage();

            try
            {
                response = await HttpClient.SendAsync(httpRequest);
                responseString = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                TranslateException(ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                TranslateException(response.StatusCode, responseString);
            }

            return JsonConvert.DeserializeObject<TResult>(responseString, JsonSettings);
        }

        private static HttpRequestMessage PrepareHttpRequestMessage<T>(string endpoint, Method httpMethod, T body)
        {
            var url = Config.BaseUrl + endpoint;
            var method = new HttpMethod(httpMethod.ToString());

            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = method,
                Content = new StringContent(string.Empty, Encoding.UTF8, MediaType)
            };

            httpRequest.Headers.Add("x-secretkey", Config.SecretKey);

            if (body != null)
            {
                var jsonBody = JsonConvert.SerializeObject(body, JsonSettings);
                httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, MediaType);
            }

            return httpRequest;
        }

        private static void TranslateException(HttpStatusCode statusCode, string responseString)
        {
            Error error = null;
            if (statusCode == HttpStatusCode.RequestTimeout)
            {
                error = new Error
                {
                    HttpStatus = 408,
                    Message = "Request Timeout",
                    Details = new Dictionary<string, string>(),
                    Resource = ""
                };

                throw new ApiException(error, error.Message, 0, true);
            }

            try
            {
                error = JsonConvert.DeserializeObject<Error>(responseString);
            }
            catch (Exception)
            {
                error = new Error
                {
                    HttpStatus = 0,
                    Message = responseString,
                    Details = new Dictionary<string, string>(),
                    Resource = ""
                };

                throw new ApiException(error, responseString, 0, true);
            }

            throw new ApiException(error, error.Message, error.Code, error.IsRetryable);
        }

        private static void TranslateException(Exception exception)
        {
            var error = new Error
            {
                HttpStatus = 0,
                Message = exception.Message,
                Details = new Dictionary<string, string>(),
                Resource = ""
            };

            throw new ApiException(error, error.Message, 0, true);
        }
    }
}