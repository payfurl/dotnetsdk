using System.Threading.Tasks;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface ICharge
    {
        ChargeData CreateWithCard(NewChargeCard newCharge);
        Task<ChargeData> CreateWithCardAsync(NewChargeCard newCharge);
        ChargeData CreateWithCardLeastCost(NewChargeCardLeastCost newCharge);
        Task<ChargeData> CreateWithCardLeastCostAsync(NewChargeCardLeastCost newCharge);
        ChargeData CreateWithCustomer(NewChargeCustomer newCharge);
        Task<ChargeData> CreateWithCustomerAsync(NewChargeCustomer newCharge);
        ChargeData CreateWithPaymentMethod(NewChargePaymentMethod newCharge);
        Task<ChargeData> CreateWithPaymentMethodAsync(NewChargePaymentMethod newCharge);
        ChargeData CreateWithToken(NewChargeToken newCharge);
        Task<ChargeData> CreateWithTokenAsync(NewChargeToken newCharge);
        ChargeData Refund(NewRefund newCharge);
        Task<ChargeData> RefundAsync(NewRefund newCharge);
        ChargeData Capture(string chargeId, NewChargeCapture chargeCaptureData);
        Task<ChargeData> CaptureAsync(string chargeId, NewChargeCapture chargeCaptureData);
        ChargeData Void(string chargeId);
        Task<ChargeData> VoidAsync(string chargeId);
        ChargeData Single(string chargeId);
        Task<ChargeData> SingleAsync(string chargeId);
        ChargeList Search(ChargeSearch searchData);
        Task<ChargeList> SearchAsync(ChargeSearch searchData);
    }
}