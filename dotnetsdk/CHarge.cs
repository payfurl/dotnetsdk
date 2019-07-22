using evertech.sdk.Models;
using evertech.sdk.Tools;

namespace evertech.sdk
{
    public class Charge : ICharge
    {
        public ChargeData CreateWithCard(NewChargeCard newCharge)
        {
            return HttpWrapper.Call<NewChargeCard, ChargeData>("/charge/card", Method.POST, newCharge);
        }

        public ChargeData CreateWithCustomer(NewChargeCustomer newCharge)
        {
            return HttpWrapper.Call<NewChargeCustomer, ChargeData>("/charge/customer", Method.POST, newCharge);
        }
    }
}
