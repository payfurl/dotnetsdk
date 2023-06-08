using payfurl.sdk;
using Xunit;

namespace FunctionalTests;

public class ConfigTest
{
    [Theory]
    [InlineData("", Environment.LOCAL, "https://localhost:5001")]
    [InlineData("", Environment.DEVELOPMENT, "https://develop-api.payfurl.com")]
    [InlineData("", Environment.SANDBOX, "https://sandbox-api.payfurl.com")]
    [InlineData("", Environment.PROD, "https://api.payfurl.com")]
    [InlineData("secretKey1-au", Environment.LOCAL, "https://localhost:5001")]
    [InlineData("secretKey1-jp", Environment.LOCAL, "https://localhost:5001")]
    [InlineData("secretKey2-au", Environment.DEVELOPMENT, "https://develop-api-au.payfurl.com")]
    [InlineData("secretKey2-us", Environment.DEVELOPMENT, "https://develop-api-us.payfurl.com")]
    [InlineData("secretKey2-jp", Environment.DEVELOPMENT, "https://develop-api-jp.payfurl.com")]
    [InlineData("secretKey2-au", Environment.SANDBOX, "https://sandbox-api-au.payfurl.com")]
    [InlineData("secretKey2-us", Environment.SANDBOX, "https://sandbox-api-us.payfurl.com")]
    [InlineData("secretKey2-au", Environment.PROD, "https://api-au.payfurl.com")]
    [InlineData("secretKey2-us", Environment.PROD, "https://api-us.payfurl.com")]
    [InlineData("secretKey2-aU", Environment.PROD, "https://api-au.payfurl.com")]
    [InlineData("secretKey2-jp", Environment.PROD, "https://api.payfurl.com")]
    [InlineData("secretKey2-123", Environment.PROD, "https://api.payfurl.com")]
    public void TestSetup(string secretKey, Environment environment, string expectedBaseUrl)
    {
        // Arrange
        // Act
        Config.Setup(secretKey, environment);

        // Assert
        Assert.Equal(expectedBaseUrl, Config.BaseUrl);
    }
}