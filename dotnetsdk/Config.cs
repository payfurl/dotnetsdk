using System;

namespace evertech.sdk
{
    public enum Environment
    {
        LOCAL,
        SANDBOX,
        PROD
    }

    public static class Config
    {
        public static string BaseUrl { get; private set; }
        public static string SecretKey { get; private set; }
        public static int TimeoutMilliseconds { get; private set; }

        public static void Setup(string secretKey, Environment environment, int timeoutMilliseconds = 60000)
        {
            if (environment == Environment.LOCAL)
                BaseUrl = "https://localhost:5001";
            if (environment == Environment.SANDBOX)
                BaseUrl = "https://evertech-stage.azurewebsites.net";
            else if (environment == Environment.PROD)
                BaseUrl = "https://evertech-prod.azurewebsites.net";

            SecretKey = secretKey;
            TimeoutMilliseconds = timeoutMilliseconds;
        }
    }
}
