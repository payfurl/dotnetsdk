using evertech.sdk.Models;
using evertech.sdk.Tools;
using System.Web;

namespace evertech.sdk
{
    public class Charge : ICharge
    {
        public ChargeData CreateWithCard(NewChargeCard newCharge)
        {
            return HttpWrapper.Call<NewChargeCard, ChargeData>("/charge/card", Method.POST, newCharge);
        }

        public ChargeData CreateWitPaymentMethod(NewChargePaymentMethod newCharge)
        {
            return HttpWrapper.Call<NewChargePaymentMethod, ChargeData>("/charge/payment_method", Method.POST, newCharge);
        }

        public ChargeData CreateWithCustomer(NewChargeCustomer newCharge)
        {
            return HttpWrapper.Call<NewChargeCustomer, ChargeData>("/charge/customer", Method.POST, newCharge);
        }
        public ChargeList Search(ChargeSearch searchData)
        {
            // TODO: move into a shared class
            var queryString = "";
                
            if (searchData.Skip.HasValue)
                queryString = "Skip=" + searchData.Skip.Value;

            if (searchData.Limit.HasValue)
                queryString = "Limit=" + searchData.Limit.Value;

            if (!string.IsNullOrWhiteSpace(searchData.Reference))
                queryString = "Reference=" + HttpUtility.UrlEncode(searchData.Reference);

            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            return HttpWrapper.Call<string, ChargeList>("/charge" + queryString, Method.GET, null);
        }
    }
}
