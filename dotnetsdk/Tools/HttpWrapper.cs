using evertech.sdk.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace evertech.sdk.Tools
{
    public enum Method
    {
        GET,
        POST
    }

    public static class HttpWrapper
    {
        static HttpWrapper()
        {
            // enfore min TLS 1.2
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
        }
        
        public static string Call(string endpoint, Method httpMethod, string jsonBody)
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

            return result;
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
                error = JsonConvert.DeserializeObject<Error>(result);
            }
            throw new ApiException(error, error.Message, exception);
        }
    }
}
