namespace payfurl.sdk
{
    public enum Environment
    {
        LOCAL,
        DEVELOPMENT,
        SANDBOX,
        PROD
    }

    public static class Config
    {
        public static string BaseUrl { get; private set; }
        public static string SecretKey { get; private set; }
        public static int TimeoutMilliseconds { get; private set; }
        public static Environment Environment { get; private set; }

        public static void Setup(string secretKey, Environment environment, int timeoutMilliseconds = 60000)
        {
            switch (environment)
            {
                case Environment.LOCAL:
                    BaseUrl = "https://localhost:5001";
                    break;
                case Environment.SANDBOX:
                    BaseUrl = "https://sandbox-api.payfurl.com";
                    break;
                case Environment.PROD:
                    BaseUrl = "https://api.payfurl.com";
                    break;
            }
            
            SecretKey = secretKey;
            TimeoutMilliseconds = timeoutMilliseconds;
        }
    }
}
