﻿using System;
using System.Collections.Generic;

namespace payfurl.sdk
{
    public enum Environment
    {
        LOCAL,
        DEVELOPMENT,
        SANDBOX,
        PROD
    }

    public enum Region
    {
        AU,
        JP,
        US,
        EU,
        NONE
    }

    public static class Config
    {
        public static string BaseUrl { get; private set; }
        public static string SecretKey { get; private set; }
        public static int TimeoutMilliseconds { get; private set; }
        public static Environment Environment { get; private set; }

        private static readonly Dictionary<(Region, Environment), string> EnvConfigToUrlMapping =
            new Dictionary<(Region, Environment), string>
            {
                { (Region.NONE, Environment.LOCAL), "https://localhost:5001" },
                { (Region.NONE, Environment.DEVELOPMENT), "https://develop-api.payfurl.com" },
                { (Region.NONE, Environment.SANDBOX), "https://sandbox-api.payfurl.com" },
                { (Region.NONE, Environment.PROD), "https://api.payfurl.com" },

                { (Region.AU, Environment.DEVELOPMENT), "https://develop-api-au.payfurl.com" },
                { (Region.US, Environment.DEVELOPMENT), "https://develop-api-us.payfurl.com" },
                { (Region.JP, Environment.DEVELOPMENT), "https://develop-api-jp.payfurl.com" },
                { (Region.AU, Environment.SANDBOX), "https://sandbox-api-au.payfurl.com" },
                { (Region.US, Environment.SANDBOX), "https://sandbox-api-us.payfurl.com" },
                { (Region.AU, Environment.PROD), "https://api-au.payfurl.com" },
                { (Region.US, Environment.PROD), "https://api-us.payfurl.com" },
                { (Region.EU, Environment.PROD), "https://api-eu.payfurl.com" }
            };

        public static void Setup(string secretKey, Environment environment, int timeoutMilliseconds = 60000)
        {
            var regionSuffix = ExtractRegionFromKey(secretKey);
            var region = ConvertToRegion(regionSuffix);

            if (EnvConfigToUrlMapping.TryGetValue((region, environment), out string baseUrl))
            {
                BaseUrl = baseUrl;
            }
            else
            {
                EnvConfigToUrlMapping.TryGetValue((Region.NONE, environment), out string baseUrlDefault);
                BaseUrl = baseUrlDefault;
            }

            SecretKey = secretKey;
            TimeoutMilliseconds = timeoutMilliseconds;
        }

        private static string ExtractRegionFromKey(string key)
        {
            const string keyRegionSeparator = "-";

            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            var parts = key.Split(new[] { keyRegionSeparator }, StringSplitOptions.None);

            return parts.Length < 2 ? null : parts[1].ToLower();
        }

        private static Region ConvertToRegion(string regionStr)
        {
            if (string.IsNullOrEmpty(regionStr))
            {
                return Region.NONE;
            }

            return Enum.TryParse(regionStr.ToUpper(), out Region region) ? region : Region.NONE;
        }
    }
}