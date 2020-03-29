using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface ICharge
    {
        ChargeData CreateWithCard(NewChargeCard newCharge);
        ChargeData CreateWithCustomer(NewChargeCustomer newCharge);
        ChargeData Refund(NewRefund newCharge);
    }
}