using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface IPaymentMethod
    {
        Checkout Checkout(NewCheckout newCheckout);
        PaymentMethodData CreatePaymentMethodWithVault(NewPaymentMethodVault newPaymentMethodVault);
        PaymentMethodData CreatePaymentMethodWithCard(NewPaymentMethodCard newNewPaymentMethodCard);
    }
}