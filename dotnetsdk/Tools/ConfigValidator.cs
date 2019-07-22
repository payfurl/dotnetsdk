using System;

namespace evertech.sdk.Tools
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConfigValidator : Attribute
    {
        public ConfigValidator()
        {
            if (string.IsNullOrEmpty(Config.SecretKey))
                throw new InvalidOperationException("Configuration not initialised. Call Config.Setup() first.");
        }
    }
}
