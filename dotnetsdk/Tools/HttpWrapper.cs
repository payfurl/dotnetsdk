using payfurl.sdk.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace payfurl.sdk.Tools
{
    public enum Method
    {
        GET,
        POST,
        DELETE
    }

    public static class HttpWrapper
    {

        private static JsonSerializerSettings _jsonSettings;

        static HttpWrapper()
        {
            // enfore min TLS 1.2
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
            _jsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Utc };
        }
        
        public static R Call<T, R>(string endpoint, Method httpMethod, T body)
        {
            var url = Config.BaseUrl + endpoint;
            var httpRequest = HttpWebRequest.Create((string)url);

            httpRequest.ContentType = "application/json";
            httpRequest.Headers.Add("x-secretkey", Config.SecretKey);

            httpRequest.Method = httpMethod.ToString();
            httpRequest.Timeout = Config.TimeoutMilliseconds;

            string result = "";

            // if we have a body
            if (httpMethod == Method.POST)
            {
                var jsonBody = JsonConvert.SerializeObject(body, _jsonSettings);
                using (var stream = httpRequest.GetRequestStream())
                {
                    if (jsonBody == null)
                        jsonBody = "";

                    var data = Encoding.UTF8.GetBytes(jsonBody);
                    stream.Write(data, 0, jsonBody.Length);
                }
            }

            WebResponse response = null;
            try
            {
                response = httpRequest.GetResponse();
            }

            catch (WebException ex)
            {
                TranslateException(ex);
            }

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<R>(result, _jsonSettings);
        }

        private static void TranslateException(WebException exception)
        {
            Error error = null;
            if (exception.Response == null)
            {
                new Error()
                {
                    HttpStatus = 408,
                    Message = "Request Timeout",
                    Details = new System.Collections.Generic.Dictionary<string, string>(),
                    Resource = ""
                };
            }

            using (var reader = new StreamReader(exception.Response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                try
                {
                    error = JsonConvert.DeserializeObject<Error>(result);
                }
                catch (Exception)
                {
                    new Error()
                    {
                        HttpStatus = 0,
                        Message = result,
                        Details = new System.Collections.Generic.Dictionary<string, string>(),
                        Resource = ""
                    };
                    throw new ApiException(error, result);
                }
            }
            throw new ApiException(error, error.Message, exception);
        }
    }
}
