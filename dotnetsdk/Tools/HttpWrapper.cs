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
        
        public static string Call(string endpoint, Method method, string json)
        {
            var url = Config.BaseUrl + endpoint;
            var request = HttpWebRequest.Create((string)url);

            request.ContentType = "application/json";
            request.Headers.Add("x-secretkey", Config.SecretKey);

            request.Method = method.ToString();
            request.Timeout = Config.TimeoutMilliseconds;

            string result = "";

            // if we have a body
            if (method == Method.POST)
            {
                using (var stream = request.GetRequestStream())
                {
                    if (json == null)
                        json = "";

                    var data = Encoding.UTF8.GetBytes(json);
                    stream.Write(data, 0, json.Length);
                }
            }

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }

            catch (WebException)
            {
                // TODO: handle this
            }

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
