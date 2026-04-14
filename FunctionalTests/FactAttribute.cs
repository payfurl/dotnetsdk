using System;

namespace FunctionalTests;

public class FactAttribute : Xunit.FactAttribute
{
    public FactAttribute()
    {
        if (!IntegrationTestSettings.ShouldRunIntegrationTests())
        {
            Skip = "Integration tests are disabled. Set RUN_INTEGRATION_TESTS=true to enable them.";
        }
    }
}

internal static class IntegrationTestSettings
{
    public static bool ShouldRunIntegrationTests()
    {
        return IsEnabled("RUN_INTEGRATION_TESTS") ||
               IsEnabled("RunIntegrationTests") ||
               IsEnabled("PAYFURL_RUN_INTEGRATION_TESTS");
    }

    private static bool IsEnabled(string variableName)
    {
        return bool.TryParse(Environment.GetEnvironmentVariable(variableName), out var enabled) && enabled;
    }
}
