using evertech.sdk.Models;
using System.Collections.Generic;

namespace evertech.sdk
{
    public interface IPaymentMethod
    {
        List<PaymentMethodData> GetForCustomer(string customerId);
    }
}