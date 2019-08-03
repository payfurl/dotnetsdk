using evertech.sdk.Models;
using evertech.sdk.Tools;
using System.Collections.Generic;
using System.Web;

namespace evertech.sdk
{
    public class PaymentMethod
    {
        public List<PaymentMethod> GetForCustomer(string customerId)
        {
            customerId = HttpUtility.UrlEncode(customerId);

            return HttpWrapper.Call<string, List<PaymentMethod>>("/payment_method/customer/" + customerId, Method.GET, null);
        }
    }
}
