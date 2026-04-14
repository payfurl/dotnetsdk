using System;
using System.Collections.Generic;
using System.Web;
using System.Threading.Tasks;
using payfurl.sdk.Helpers;
using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class Settlement : ISettlement
    {
        public List<payfurl.sdk.Models.Settlement> GetSettlements(DateTime startDate, DateTime? endDate = null)
        {
            return AsyncHelper.RunSync(() => GetSettlementsAsync(startDate, endDate));
        }

        public async Task<List<payfurl.sdk.Models.Settlement>> GetSettlementsAsync(DateTime startDate, DateTime? endDate = null)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("startDate", startDate.ToString("yyyy-MM-dd"));
            if (endDate.HasValue)
            {
                queryString.Add("endDate", endDate.Value.ToString("yyyy-MM-dd"));
            }

            return await HttpWrapper.CallAsync<string, List<payfurl.sdk.Models.Settlement>>(
                "/settlements?" + queryString, Method.GET, null);
        }
    }
}
