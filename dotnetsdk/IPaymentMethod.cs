using payfurl.sdk.Models;
using System.Collections.Generic;

namespace payfurl.sdk
{
    public interface IPaymentMethod
    {
        List<PaymentMethodData> GetForCustomer(string customerId);
        string GenerateClientToken(string providerId);
    }
}