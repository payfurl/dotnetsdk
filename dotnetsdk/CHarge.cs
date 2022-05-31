using payfurl.sdk.Models;
using payfurl.sdk.Tools;
using System.Web;

namespace payfurl.sdk
{
    public class Charge : ICharge
    {
        public ChargeData CreateWithCard(NewChargeCard newCharge)
        {
            return HttpWrapper.Call<NewChargeCard, ChargeData>("/charge/card", Method.POST, newCharge);
        }
        
        public ChargeData CreateWithCardLeastCost(NewChargeCardLeastCost newCharge)
        {
            return HttpWrapper.Call<NewChargeCardLeastCost, ChargeData>("/charge/card/least_cost", Method.POST, newCharge);
        }

        public ChargeData CreateWithPaymentMethod(NewChargePaymentMethod newCharge)
        {
            return HttpWrapper.Call<NewChargePaymentMethod, ChargeData>("/charge/payment_method", Method.POST, newCharge);
        }

        public ChargeData CreateWithCustomer(NewChargeCustomer newCharge)
        {
            return HttpWrapper.Call<NewChargeCustomer, ChargeData>("/charge/customer", Method.POST, newCharge);
        }

        public ChargeData CreateWithToken(NewChargeToken newCharge)
        {
            return HttpWrapper.Call<NewChargeToken, ChargeData>("/charge/token", Method.POST, newCharge);
        }

        public ChargeData Single(string chargeId)
        {
            return HttpWrapper.Call<string, ChargeData>("/charge/" + chargeId, Method.GET, null);
        }

        public ChargeList Search(ChargeSearch searchData)
        {
            // TODO: move into a shared class to handle formatting
            var queryString = "";
                
            if (searchData.Skip.HasValue)
                queryString = "Skip=" + searchData.Skip.Value;

            if (searchData.Limit.HasValue)
                queryString = "Limit=" + searchData.Limit.Value;

            if (!string.IsNullOrWhiteSpace(searchData.Reference))
                queryString = "Reference=" + HttpUtility.UrlEncode(searchData.Reference);

            if (!string.IsNullOrWhiteSpace(searchData.PaymentMethodId))
                queryString = "PaymentMethodId=" + HttpUtility.UrlEncode(searchData.PaymentMethodId);

            if (searchData.AmountGreaterThan.HasValue)
                queryString = "AmountGreaterThan=" + HttpUtility.UrlEncode(searchData.AmountGreaterThan.Value.ToString());

            if (searchData.AmountLessThan.HasValue)
                queryString = "AmountLessThan=" + HttpUtility.UrlEncode(searchData.AmountLessThan.Value.ToString());

            if (!string.IsNullOrWhiteSpace(searchData.CustomerId))
                queryString = "CustomerId=" + HttpUtility.UrlEncode(searchData.CustomerId);

            if (!string.IsNullOrWhiteSpace(searchData.Status))
                queryString = "Status=" + HttpUtility.UrlEncode(searchData.Status);

            if (searchData.AddedAfter.HasValue)
                queryString = "AddedAfter=" + HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (searchData.AddedBefore.HasValue)
                queryString = "AddedBefore=" + HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss"));

            if (!string.IsNullOrWhiteSpace(searchData.SortBy))
                queryString = "SortBy=" + searchData.SortBy;

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return HttpWrapper.Call<string, ChargeList>("/charge" + queryString, Method.GET, null);
        }

        public ChargeData Refund(NewRefund newCharge)
        {
            var queryString = "";
            if (newCharge.RefundAmount.HasValue)
                queryString = "?amount=" + newCharge.RefundAmount.Value;

            return HttpWrapper.Call<string, ChargeData>("/charge/" + newCharge.ChargeId + queryString, Method.DELETE, null);
        }
    }
}
