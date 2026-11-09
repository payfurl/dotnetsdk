using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Disputes the gateways report: <c>/dispute</c>.</summary>
public sealed class Disputes
{
    private readonly ApiClient _api;

    internal Disputes(ApiClient api) => _api = api;

    /// <summary><c>GET /dispute/{disputeId}</c></summary>
    public Task<Dispute> GetAsync(string disputeId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<Dispute>("/dispute/" + ApiClient.Id(disputeId, nameof(disputeId)), null, cancellationToken);

    /// <summary><c>GET /dispute</c></summary>
    public Task<DisputeList> SearchAsync(DisputeSearch? search = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<DisputeList>("/dispute", search?.ToQuery(), cancellationToken);
}
