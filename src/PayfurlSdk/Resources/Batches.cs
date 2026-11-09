using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Batches of charges: <c>/batch</c>.</summary>
public sealed class Batches
{
    private readonly ApiClient _api;

    internal Batches(ApiClient api) => _api = api;

    /// <summary><c>POST /batch/transaction/payment_method</c>: <c>batch.Batch</c> is the CSV of charges.</summary>
    public Task<BatchStatus> CreateWithPaymentMethodsAsync(NewTransactionPaymentMethod batch, CancellationToken cancellationToken = default) =>
        _api.PostAsync<BatchStatus>("/batch/transaction/payment_method", batch, cancellationToken);

    /// <summary><c>GET /batch/{batchId}</c>: the batch with its results.</summary>
    public Task<BatchData> GetAsync(string batchId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<BatchData>("/batch/" + ApiClient.Id(batchId, nameof(batchId)), null, cancellationToken);

    /// <summary><c>GET /batch/{batchId}/status</c>: the batch's progress, without its results.</summary>
    public Task<BatchStatus> GetStatusAsync(string batchId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<BatchStatus>("/batch/" + ApiClient.Id(batchId, nameof(batchId)) + "/status", null, cancellationToken);

    /// <summary><c>GET /batch</c></summary>
    public Task<BatchList> SearchAsync(BatchSearch? search = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<BatchList>("/batch", search?.ToQuery(), cancellationToken);
}
