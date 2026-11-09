using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Transfers to sub-merchants: <c>/transfer</c>.</summary>
public sealed class Transfers
{
    private readonly ApiClient _api;

    internal Transfers(ApiClient api) => _api = api;

    /// <summary><c>POST /transfer</c>: one transfer per item of <c>transfers.Transfers</c>, linked to a charge or not.</summary>
    public Task<List<TransferData>> CreateAsync(NewTransferGroup transfers, CancellationToken cancellationToken = default) =>
        _api.PostAsync<List<TransferData>>("/transfer", transfers, cancellationToken);

    /// <summary><c>GET /transfer/{transferId}</c></summary>
    public Task<TransferData> GetAsync(string transferId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<TransferData>("/transfer/" + ApiClient.Id(transferId, nameof(transferId)), null, cancellationToken);

    /// <summary><c>GET /transfer</c></summary>
    public Task<TransferList> SearchAsync(TransferSearch? search = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<TransferList>("/transfer", search?.ToQuery(), cancellationToken);
}
