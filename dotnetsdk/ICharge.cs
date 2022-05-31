using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface ICharge
    {
        ChargeData CreateWithCard(NewChargeCard newCharge);
        ChargeData CreateWithCardLeastCost(NewChargeCardLeastCost newCharge);
        ChargeData CreateWithCustomer(NewChargeCustomer newCharge);
        ChargeData CreateWithPaymentMethod(NewChargePaymentMethod newCharge);
        ChargeData CreateWithToken(NewChargeToken newCharge);
        ChargeData Refund(NewRefund newCharge);
        ChargeData Single(string chargeId);
        ChargeList Search(ChargeSearch searchData);
    }
}