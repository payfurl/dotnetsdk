using payfurl.sdk.Models;
using payfurl.sdk.Tools;
using System.Collections.Generic;
using System.Web;

namespace payfurl.sdk
{
    public class PaymentMethod : IPaymentMethod
    {
        public List<PaymentMethodData> GetForCustomer(string customerId)
        {
            customerId = HttpUtility.UrlEncode(customerId);

            return HttpWrapper.Call<string, List<PaymentMethodData>>("/payment_method/customer/" + customerId, Method.GET, null);
        }

        public string GenerateClientToken(string providerId)
        {
            providerId = HttpUtility.UrlEncode(providerId);

            return HttpWrapper.Call<string, string>("/payment_method/client_token/" + providerId, Method.GET, null);
        }

        public Checkout Checkout(NewCheckout newCheckout)
        {
            return HttpWrapper.Call<NewCheckout, Checkout>("/payment_method/checkout", Method.POST, newCheckout);
        }
    }
}
