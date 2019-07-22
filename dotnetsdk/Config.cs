using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace evertech.sdk
{
    public static class Config
    {
        public static string BaseUrl { get; private set; }
        public static string SecretKey { get; private set; }
        public static int TimeoutMilliseconds { get; private set; }

        // TODO: add support for environments and setting the URL based on that
        public static void Setup(string secretKey)
        {
            BaseUrl = "https://api20190721083325.azurewebsites.net";
            SecretKey = secretKey;
            TimeoutMilliseconds = 30000;
        }
    }
}
