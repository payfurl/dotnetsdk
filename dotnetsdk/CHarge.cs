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
            throw new NotImplementedException();
        }

        [ConfigValidator]
        public ChargeData CreateWithCustomer(NewChargeCard newCharge)
        {
            throw new NotImplementedException();
        }
    }
}
