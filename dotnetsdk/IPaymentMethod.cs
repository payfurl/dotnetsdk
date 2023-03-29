using System.Threading.Tasks;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface IPaymentMethod
    {
        PaymentMethodData CreatePaymentMethodWithVault(NewPaymentMethodVault newPaymentMethodVault);
        Task<PaymentMethodData> CreatePaymentMethodWithVaultAsync(NewPaymentMethodVault newPaymentMethodVault);
        PaymentMethodData CreatePaymentMethodWithCard(NewPaymentMethodCard newNewPaymentMethodCard);
        Task<PaymentMethodData> CreatePaymentMethodWithCardAsync(NewPaymentMethodCard newNewPaymentMethodCard);
        PaymentMethodData CreatePaymentMethodWithPayto(NewPayToAgreement newNewPaymentMethodCard);
        Task<PaymentMethodData> CreatePaymentMethodWithPaytoAsync(NewPayToAgreement newNewPaymentMethodCard);
        PaymentMethodData Single(string paymentMethodId);
        Task<PaymentMethodData> SingleAsync(string paymentMethodId);
        PaymentMethodList Search(PaymentMethodSearch searchData);
        Task<PaymentMethodList> SearchAsync(PaymentMethodSearch searchData);
    }
}
