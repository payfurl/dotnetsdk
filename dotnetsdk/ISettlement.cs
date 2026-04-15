using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface ISettlement
    {
        List<payfurl.sdk.Models.Settlement> GetSettlements(DateTime startDate, DateTime? endDate = null);
        Task<List<payfurl.sdk.Models.Settlement>> GetSettlementsAsync(DateTime startDate, DateTime? endDate = null);
    }
}
