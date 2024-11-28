using System;
using System.Collections.Generic;
using payfurl.sdk.Models;
using payfurl.sdk.Models.Batch;
using payfurl.sdk.Models.Subscriptions;
using Xunit;

namespace FunctionalTests;

public class Subscription : BaseTest
{
    private static readonly CardRequestInformation CardRequestInformation = new()
    {
        CardNumber = "4111111111111111",
        ExpiryDate = "12/35",
        Ccv = "123"
    };

    private NewPaymentMethodCard GetNewPaymentMethod()
    {
        return new NewPaymentMethodCard
        {
            ProviderId = GetProviderId(),
            PaymentInformation = CardRequestInformation
        };
    }
    
    private SubscriptionCreate GetNewSubscription(string paymentMethodId)
    {
        return new SubscriptionCreate
        {
            EndAfter = new SubscriptionEnd()
            {
                Count = 2
            },
            Retry = new SubscriptionRetryPolicy()
            {
                Maximum = 3,
                Frequency = 1,
                Interval = SubscriptionRetryPolicy.SubscriptionRetryInterval.Day
            },
            Webhook = new WebhookConfig()
            {
                Url = "https://example.com/webhoo",
                Authorization = "secret"
            },
            PaymentMethodId = paymentMethodId,
            Amount = 100,
            Currency = "USD",
            Interval = payfurl.sdk.Models.Subscriptions.Subscription.SubscriptionInterval.Month,
            Frequency = 1
        };
    }
    
    [Fact]
    public void CreateSubscription()
    {
        
        var svcPaymentMethod = new payfurl.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new payfurl.sdk.Subscription();
        var result = svc.CreateSubscription(GetNewSubscription(resultPaymentMethod.PaymentMethodId));

        Assert.NotNull(result);
        Assert.Equal(result.PaymentMethodId, resultPaymentMethod.PaymentMethodId);
        Assert.Equal(result.Status, payfurl.sdk.Models.Subscriptions.Subscription.SubscriptionStatus.Active);
    }
    
    [Fact]
    public void GetSubscription()
    {
        var svcPaymentMethod = new payfurl.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new payfurl.sdk.Subscription();
        var subscription = svc.CreateSubscription(GetNewSubscription(resultPaymentMethod.PaymentMethodId));
        
        var resultSubscription = svc.GetSubscription(subscription.SubscriptionId);
        
        Assert.NotNull(resultSubscription);
        Assert.Equal(resultSubscription.PaymentMethodId, resultPaymentMethod.PaymentMethodId);
        Assert.Equal(resultSubscription.Status, payfurl.sdk.Models.Subscriptions.Subscription.SubscriptionStatus.Active);
    }
    
    [Fact]
    public void DeleteSubscription()
    {
        var svcPaymentMethod = new payfurl.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new payfurl.sdk.Subscription();
        var subscription = svc.CreateSubscription(GetNewSubscription(resultPaymentMethod.PaymentMethodId));
        
        var resultSubscription = svc.DeleteSubscription(subscription.SubscriptionId);
        
        Assert.NotNull(resultSubscription);
        Assert.Equal(resultSubscription.PaymentMethodId, resultPaymentMethod.PaymentMethodId);
        Assert.Equal(resultSubscription.Status, payfurl.sdk.Models.Subscriptions.Subscription.SubscriptionStatus.Cancelled);
    }
    
    [Fact]
    public void SearchSubscription()
    {
        var svcPaymentMethod = new payfurl.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new payfurl.sdk.Subscription();
        var newSubscription = GetNewSubscription(resultPaymentMethod.PaymentMethodId);
        newSubscription.Amount = 200;
        var subscription = svc.CreateSubscription(newSubscription);
        
        var subscriptionList = svc.SearchSubscription(new SubscriptionSearch(){ AmountGreaterThan = 150});
        
        Assert.NotNull(subscriptionList);
        Assert.Equal(subscriptionList.Count, 1);
        Assert.Equal(subscriptionList.Subscriptions[0].SubscriptionId,subscription.SubscriptionId);
    }
    
    [Fact]
    public void UpdateSubscription()
    {
        
        var svcPaymentMethod = new payfurl.sdk.PaymentMethod();
        var newPaymentMethod = GetNewPaymentMethod(); 
        newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
        var resultPaymentMethod = svcPaymentMethod.CreatePaymentMethodWithCard(newPaymentMethod);
        
        var svc = new payfurl.sdk.Subscription();
        var result = svc.CreateSubscription(GetNewSubscription(resultPaymentMethod.PaymentMethodId));

        Assert.Equal(100, result.Amount);
        Assert.Equal("USD", result.Currency);
        
        result = svc.UpdateSubscription(result.SubscriptionId, new SubscriptionUpdate()
        {
            Amount = 200,
            Currency = "AUD",
        });
        
        Assert.NotNull(result);
        Assert.Equal(200, result.Amount);
        Assert.Equal("AUD", result.Currency);
        Assert.Equal(payfurl.sdk.Models.Subscriptions.Subscription.SubscriptionInterval.Day, result.Interval);
        Assert.Null(result.EndAfter);
        Assert.Null(result.Retry);
        Assert.Null(result.Webhook);
    }
}