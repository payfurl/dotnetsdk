using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Settlements the gateways report: <c>/settlements</c>.</summary>
public sealed class Settlements
{
    private readonly ApiClient _api;

    internal Settlements(ApiClient api) => _api = api;

    /// <summary><c>GET /settlements</c>: from <paramref name="startDate"/> up to <paramref name="endDate"/> (now when <c>null</c>), by date: the time of day is ignored.</summary>
    public Task<List<Settlement>> SearchAsync(DateTime startDate, DateTime? endDate = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<List<Settlement>>("/settlements", Query.Of(("startDate", startDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)), ("endDate", endDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))), cancellationToken);
}
