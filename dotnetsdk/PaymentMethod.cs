using payfurl.sdk.Models;
using payfurl.sdk.Tools;

namespace payfurl.sdk
{
    public class PaymentMethod : IPaymentMethod
    {
        public Checkout Checkout(NewCheckout newCheckout)
        {
            return HttpWrapper.Call<NewCheckout, Checkout>("/payment_method/checkout", Method.POST, newCheckout);
        }

        public PaymentMethodData CreatePaymentMethodWithVault(NewPaymentMethodVault newPaymentMethodVault)
        {
            return HttpWrapper.Call<NewPaymentMethodVault, PaymentMethodData>("payment_method/vault", Method.POST, newPaymentMethodVault);
        }

        public PaymentMethodData CreatePaymentMethodWithCard(NewPaymentMethodCard newPaymentMethodCard)
        {
            return HttpWrapper.Call<NewPaymentMethodCard, PaymentMethodData>("payment_method/card", Method.POST, newPaymentMethodCard);
        }
    }
}
