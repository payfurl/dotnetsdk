using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Cards stored in payFURL's vault: <c>/vault</c>.</summary>
public sealed class Vaults
{
    private readonly ApiClient _api;

    internal Vaults(ApiClient api) => _api = api;

    /// <summary><c>POST /vault</c></summary>
    public Task<VaultData> CreateAsync(NewVault vault, CancellationToken cancellationToken = default) =>
        _api.PostAsync<VaultData>("/vault", vault, cancellationToken);

    /// <summary><c>GET /vault/{vaultId}</c>: with the full card number and CVV, so handle the result as card data (PCI DSS).</summary>
    public Task<VaultDataWithPCI> GetAsync(string vaultId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<VaultDataWithPCI>("/vault/" + ApiClient.Id(vaultId, nameof(vaultId)), null, cancellationToken);

    /// <summary><c>DELETE /vault/{vaultId}</c></summary>
    public Task<VaultData> DeleteAsync(string vaultId, CancellationToken cancellationToken = default) =>
        _api.DeleteAsync<VaultData>("/vault/" + ApiClient.Id(vaultId, nameof(vaultId)), null, cancellationToken);
}
