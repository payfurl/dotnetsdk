using payfurl.sdk.Models;
using System.Collections.Generic;

namespace payfurl.sdk
{
    public interface IPaymentMethod
    {
        Checkout Checkout(NewCheckout newCheckout);
        PaymentMethodList Search(PaymentMethodSearch searchData);
        PaymentMethodData Single(string paymentMethodId);
    }
}