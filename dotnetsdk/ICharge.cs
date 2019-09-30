using evertech.sdk.Models;

namespace evertech.sdk
{
    public interface ICharge
    {
        ChargeData CreateWithCard(NewChargeCard newCharge);
        ChargeData CreateWithCustomer(NewChargeCustomer newCharge);
        ChargeData Refund(NewRefund newCharge);
    }
}