using System;
using payfurl.sdk.Models;
using Xunit;

namespace FunctionalTests;

public class WebhookSubscription : BaseTest
{
    private readonly payfurl.sdk.WebhookSubscription _service;

    public WebhookSubscription()
    {
        _service = new payfurl.sdk.WebhookSubscription();
    }
    
    private WebhookSubscriptionData CreateWebhookSubscription()
    {
        return _service.CreateWebhookSubscription(new NewWebhookSubscription
        {
            Url = "https://webhook.site",
            Types = [WebhookType.Transaction],
        });
    }
    
    [Fact]
    public void CreateSubscription()
    {
        
        var result = CreateWebhookSubscription();

        Assert.NotNull(result);
        Assert.NotNull(result.WebhookSubscriptionId);
    }
    
    [Fact]
    public void GetSubscription()
    {
        var webhook = CreateWebhookSubscription();
        
        var result = _service.GetWebhookSubscription(webhook.WebhookSubscriptionId);
        
        Assert.NotNull(result);
        Assert.Equal(webhook.WebhookSubscriptionId, result.WebhookSubscriptionId);
    }
    
    [Fact]
    public void DeleteSubscription()
    {
        var webhook = CreateWebhookSubscription();
        
        var result = _service.DeleteWebhookSubscription(webhook.WebhookSubscriptionId);
        
        Assert.NotNull(result);
        Assert.Equal(webhook.WebhookSubscriptionId, result.WebhookSubscriptionId);
    }
    
    [Fact]
    public void SearchSubscription()
    {
        var webhook = CreateWebhookSubscription();
        
        var result = _service.SearchWebhookSubscription(new WebhookSubscriptionSearch
        {
            Id = webhook.WebhookSubscriptionId,
        });
        
        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
        Assert.Equal(webhook.WebhookSubscriptionId, result.WebhookSubscriptions[0].WebhookSubscriptionId);
    }

    [Fact]
    public void SearchSubscriptionWithAdditionalFilters()
    {
        var webhook = CreateWebhookSubscription();

        var result = _service.SearchWebhookSubscription(new WebhookSubscriptionSearch
        {
            Id = webhook.WebhookSubscriptionId,
            AddedAfter = DateTime.UtcNow.AddDays(-1),
            AddedBefore = DateTime.UtcNow.AddDays(1),
            Sort = WebhookSubscriptionSearch.SortBy.Date,
            Limit = 10,
        });

        Assert.NotNull(result);
        Assert.Contains(result.WebhookSubscriptions, x => x.WebhookSubscriptionId == webhook.WebhookSubscriptionId);
    }

}
