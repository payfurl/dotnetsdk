using payfurl.sdk.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using payfurl.sdk.Helpers;

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

        public static async Task<TResult> CallAsync<TInput, TResult>(string endpoint, Method httpMethod, TInput body, Dictionary<string, string> headers = null)
        {
            var httpRequest = PrepareHttpRequestMessage<TInput>(endpoint, httpMethod, body, headers);

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
                TranslateException(null, response.StatusCode, responseString);
            }

            return JsonConvert.DeserializeObject<TResult>(responseString, JsonSettings);
        }

        private static HttpRequestMessage PrepareHttpRequestMessage<T>(string endpoint, Method httpMethod, T body, Dictionary<string, string> headers)
        {
            var url = Config.BaseUrl + endpoint;
            var method = new HttpMethod(httpMethod.ToString());

            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = method,
                Content = new StringContent(string.Empty, Encoding.UTF8, MediaType),
            };

            httpRequest.Headers.Add("x-secretkey", Config.SecretKey);
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpRequest.Headers.Add(header.Key, header.Value);
                }
            }

            if (body != null)
            {
                var jsonBody = JsonConvert.SerializeObject(body, JsonSettings);
                httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, MediaType);
            }

            return httpRequest;
        }

        private static void TranslateException(Exception exception, HttpStatusCode? statusCode = null, string responseString = null)
        {
            var error = BuildDefaultError();

            if (exception != null && IsTimeoutException(exception) || (statusCode.HasValue && (statusCode == HttpStatusCode.RequestTimeout || statusCode == HttpStatusCode.GatewayTimeout)))
            {
                var timeoutError = BuildTimeoutError();

                throw new ApiException(timeoutError, timeoutError.Message, timeoutError.Code, true);
            }

            if (!string.IsNullOrEmpty(responseString))
            {
                var responseError = JsonConvert.DeserializeObject<Error>(responseString);

                throw new ApiException(responseError, responseError?.Message, responseError.Code, true);
            }

            throw new ApiException(error, error.Message, error.Code, true);
        }

        private static bool IsTimeoutException(Exception exception)
        {
            return exception is TaskCanceledException ||
                   (exception is HttpRequestException httpException &&
                    httpException.InnerException is IOException ioException
                    && ioException.InnerException is SocketException socketException
                    && socketException.SocketErrorCode == SocketError.ConnectionReset);
        }

        private static Error BuildDefaultError()
        {
            var error = new Error
            {
                HttpStatus = 500,
                Code = ErrorCodeType.UnknownError,
                Type = ErrorCodeTypeTranslator.GetErrorCodeUrlByCode(ErrorCodeType.UnknownError),
                Message = "Unknown error",
                Details = new Dictionary<string, string>(),
                Resource = ""
            };

            return error;
        }

        private static Error BuildTimeoutError()
        {
            var error = new Error
            {
                HttpStatus = 408,
                Code = ErrorCodeType.Timeout,
                Type = ErrorCodeTypeTranslator.GetErrorCodeUrlByCode(ErrorCodeType.Timeout),
                Message = "Request Timeout",
                Details = new Dictionary<string, string>(),
                Resource = ""
            };

            return error;
        }
    }
}