﻿using System.Threading.Tasks;
using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface IPaymentMethod
    {
        Checkout Checkout(NewCheckout newCheckout);
        Task<Checkout> CheckoutAsync(NewCheckout newCheckout);
        PaymentMethodData CreatePaymentMethodWithVault(NewPaymentMethodVault newPaymentMethodVault);
        Task<PaymentMethodData> CreatePaymentMethodWithVaultAsync(NewPaymentMethodVault newPaymentMethodVault);
        PaymentMethodData CreatePaymentMethodWithCard(NewPaymentMethodCard newNewPaymentMethodCard);
        Task<PaymentMethodData> CreatePaymentMethodWithCardAsync(NewPaymentMethodCard newNewPaymentMethodCard);
        PaymentMethodData Single(string paymentMethodId);
        Task<PaymentMethodData> SingleAsync(string paymentMethodId);
        PaymentMethodList Search(PaymentMethodSearch searchData);
        Task<PaymentMethodList> SearchAsync(PaymentMethodSearch searchData);
    }
}