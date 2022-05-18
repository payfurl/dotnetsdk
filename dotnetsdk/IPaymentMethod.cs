using payfurl.sdk.Models;
using System.Collections.Generic;

namespace payfurl.sdk
{
    public interface IPaymentMethod
    {
        string GenerateClientToken(string providerId);
        Checkout Checkout(NewCheckout newCheckout);
    }
}