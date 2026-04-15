using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests;

public class Settlement : BaseTest
{
    [Fact]
    public void GetSettlements()
    {
        var startDate = DateTime.UtcNow.AddYears(-10).Date;
        var endDate = DateTime.UtcNow.AddDays(1).Date;

        var svc = new payfurl.sdk.Settlement();
        var result = svc.GetSettlements(startDate, endDate);

        Assert.NotNull(result);
        Assert.All(result, settlement =>
        {
            Assert.False(string.IsNullOrWhiteSpace(settlement.SettlementId));
            Assert.InRange(settlement.SettlementDate.Date, startDate, endDate);
        });
    }

    [Fact]
    public async Task GetSettlementsAsync()
    {
        var startDate = DateTime.UtcNow.AddYears(-10).Date;

        var svc = new payfurl.sdk.Settlement();
        var result = await svc.GetSettlementsAsync(startDate);

        Assert.NotNull(result);
        Assert.All(result, settlement =>
        {
            Assert.False(string.IsNullOrWhiteSpace(settlement.SettlementId));
            Assert.True(settlement.SettlementDate.Date >= startDate);
        });
    }
}
