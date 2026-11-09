using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Card tokens the browser SDK made: <c>/token</c>.</summary>
public sealed class Tokens
{
    private readonly ApiClient _api;

    internal Tokens(ApiClient api) => _api = api;

    /// <summary><c>GET /token/{tokenId}</c></summary>
    public Task<TokenData> GetAsync(string tokenId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<TokenData>("/token/" + ApiClient.Id(tokenId, nameof(tokenId)), null, cancellationToken);

    /// <summary><c>GET /token</c></summary>
    public Task<TokenList> SearchAsync(TokenSearch? search = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<TokenList>("/token", search?.ToQuery(), cancellationToken);
}
