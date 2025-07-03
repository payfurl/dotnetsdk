using System.Threading.Tasks;
using payfurl.sdk.Models.PaymentLink;
using Xunit;

namespace FunctionalTests;

public class PaymentLink : BaseTest
{
    [Fact]
    public async Task CreatePaymentLink()
    {
        var svc = new payfurl.sdk.PaymentLink();
        var result = await svc.CreateAsync(GenerateCreatePaymentLink());
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task GetPaymentLink()
    {
        var svc = new payfurl.sdk.PaymentLink();
        var result = await svc.CreateAsync(GenerateCreatePaymentLink());
        var paymentLink = await svc.SingleAsync(result.PaymentLinkId);
        Assert.NotNull(paymentLink);
    }

    [Fact]
    public async Task SearchPaymentLink()
    {
        var svc = new payfurl.sdk.PaymentLink();
        var result = await svc.CreateAsync(GenerateCreatePaymentLink());
        var searchResult = await svc.SearchAsync(new SearchPaymentLink());
        Assert.NotNull(searchResult);
        Assert.NotEmpty(searchResult.PaymentLinks);
        Assert.Contains(searchResult.PaymentLinks, pl => pl.PaymentLinkId == result.PaymentLinkId);
    }

    private static CreatePaymentLink GenerateCreatePaymentLink()
    {
        return new CreatePaymentLink
        {
            Title = "Test Payment Link",
            Amount = 1000,
            Currency = "USD",
            Image = payfurl.sdk.Models.PaymentLink.CreatePaymentLink.EncodeImage("./100x50.png"),
        };
    }
}