using System;
using payfurl.sdk.Models.Batch;
using Xunit;

namespace FunctionalTests;

public class Batch : BaseTest
{
    [Fact]
    public void CreateTransactionWithPaymentMethod()
    {
        var description = Guid.NewGuid().ToString();
        var svc = new payfurl.sdk.Batch();
        var result = svc.CreateTransactionWithPaymentMethod(GetNewTransactionPaymentMethod(description));

        Assert.Equal(1, result.Count);
        Assert.Equal(description, result.Description);
    }
    
    [Fact]
    public void GetBatch()
    {
        var description = Guid.NewGuid().ToString();
        var svc = new payfurl.sdk.Batch();
        var batch = svc.CreateTransactionWithPaymentMethod(GetNewTransactionPaymentMethod(description));

        var result = svc.GetBatch(batch.BatchId);
        
        Assert.Equal(1, result.Count);
        Assert.Equal(description, result.Description);
        Assert.Equal("PaymentMethodId,Amount,Currency,Reference,Status,TransactionId\r\n", result.Results);
    }
    
    [Fact]
    public void GetBatchStatus()
    {
        var description = Guid.NewGuid().ToString();
        var svc = new payfurl.sdk.Batch();
        var batch = svc.CreateTransactionWithPaymentMethod(GetNewTransactionPaymentMethod(description));

        var result = svc.GetBatchStatus(batch.BatchId);
        
        Assert.Equal(1, result.Count);
        Assert.Equal(description, result.Description);
    }
    
    [Fact]
    public void SearchBatch()
    {
        var description = Guid.NewGuid().ToString("N");
        var svc = new payfurl.sdk.Batch();
        svc.CreateTransactionWithPaymentMethod(GetNewTransactionPaymentMethod(description));

        var result = svc.SearchBatch(new BatchSearch
        {
            Description = description
        });
        
        Assert.Equal(1, result.Count);
        Assert.Equal(description, result.Batches[0].Description);
    }
    
    private NewTransactionPaymentMethod GetNewTransactionPaymentMethod(string description = "Test")
    {
        return new NewTransactionPaymentMethod
        {
            Count = 1,
            Description = description,
            Batch = "PaymentMethodId,Amount,Currency,Reference\ntest,123.4,AUD,reference"
        };
    }
}