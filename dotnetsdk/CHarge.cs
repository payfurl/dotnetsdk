using evertech.sdk.Models;
using evertech.sdk.Tools;
using System;

namespace evertech.sdk
{
    public class Charge : ICharge
    {
        [ConfigValidator]
        public ChargeData CreateWithCard(NewChargeCard newCharge)
        {
            return HttpWrapper.Call<NewChargeCard, ChargeData>("/charge/card", Method.POST, newCharge);
        }

        [ConfigValidator]
        public ChargeData CreateWithCustomer(NewChargeCustomer newCharge)
        {
            return HttpWrapper.Call<NewChargeCustomer, ChargeData>("/charge/customer", Method.POST, newCharge);
        }
    }
}
