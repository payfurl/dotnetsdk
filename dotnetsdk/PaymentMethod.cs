using payfurl.sdk.Models;
using payfurl.sdk.Tools;
using System;
using System.Collections.Generic;
using System.Web;

namespace payfurl.sdk
{
    public class PaymentMethod : IPaymentMethod
    {
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
