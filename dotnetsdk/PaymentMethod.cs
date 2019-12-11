﻿using evertech.sdk.Models;
using evertech.sdk.Tools;
using System.Collections.Generic;
using System.Web;

namespace evertech.sdk
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
    }
}
