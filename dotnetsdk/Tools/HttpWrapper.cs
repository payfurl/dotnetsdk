using payfurl.sdk.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

        // PooledConnectionLifetime prevents DNS caching but is not supported with current .NET versions.
        // No client-wide timeout: each call takes Config.TimeoutMilliseconds as it is when the call starts.
        private static readonly HttpClient HttpClient = new HttpClient(/*new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(1)
        }*/)
        {
            Timeout = Timeout.InfiniteTimeSpan
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
                using (var timeout = new CancellationTokenSource(TimeSpan.FromMilliseconds(Config.TimeoutMilliseconds)))
                {
                    response = await HttpClient.SendAsync(httpRequest, timeout.Token);
                    responseString = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                TranslateException(ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                TranslateException(null, response.StatusCode, responseString);
            }

            try
            {
                return JsonConvert.DeserializeObject<TResult>(responseString, JsonSettings);
            }
            catch (JsonException ex)
            {
                var error = BuildDefaultError((int)response.StatusCode, "The API's response is not valid JSON");
                throw new ApiException(error, error.Message, error.Code, false, ex);
            }
        }

        private static HttpRequestMessage PrepareHttpRequestMessage<T>(string endpoint, Method httpMethod, T body, Dictionary<string, string> headers)
        {
            var url = Config.BaseUrl + endpoint;
            var method = new HttpMethod(httpMethod.ToString());

            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = method,
            };

            if (httpMethod == Method.POST || httpMethod == Method.PUT)
            {
                httpRequest.Content = new StringContent(string.Empty, Encoding.UTF8, MediaType);
            }

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

            if (exception != null && IsTimeoutException(exception) || (statusCode.HasValue && (statusCode == HttpStatusCode.RequestTimeout || statusCode == HttpStatusCode.GatewayTimeout)))
            {
                var timeoutError = BuildTimeoutError();

                throw new ApiException(timeoutError, timeoutError.Message, timeoutError.Code, true);
            }

            var status = statusCode.HasValue ? (int)statusCode.Value : 500;
            // A server error or a rate limit is worth retrying when the API doesn't say.
            var retryable = status >= 500 || status == 429;
            Exception parseError = null;

            if (!string.IsNullOrEmpty(responseString))
            {
                Error responseError = null;
                try
                {
                    responseError = JsonConvert.DeserializeObject<Error>(responseString, JsonSettings);
                }
                catch (JsonException ex)
                {
                    // Not the API's JSON error, such as a load balancer's HTML 502.
                    parseError = ex;
                }

                if (responseError != null)
                {
                    throw new ApiException(responseError, responseError.Message, responseError.Code, responseError.IsRetryable);
                }
            }

            var error = BuildDefaultError(status, statusCode.HasValue ? "HTTP " + status + " from the payFURL API" : "Unknown error");
            throw new ApiException(error, error.Message, error.Code, exception == null ? retryable : true, exception ?? parseError);
        }

        private static bool IsTimeoutException(Exception exception)
        {
            return exception is TaskCanceledException ||
                   (exception is HttpRequestException httpException &&
                    httpException.InnerException is IOException ioException
                    && ioException.InnerException is SocketException socketException
                    && socketException.SocketErrorCode == SocketError.ConnectionReset);
        }

        private static Error BuildDefaultError(int httpStatus = 500, string message = "Unknown error")
        {
            var error = new Error
            {
                HttpStatus = httpStatus,
                Code = ErrorCodeType.UnknownError,
                Type = ErrorCodeTypeTranslator.GetErrorCodeUrlByCode(ErrorCodeType.UnknownError),
                Message = message,
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