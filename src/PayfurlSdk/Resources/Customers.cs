using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Customers and their payment methods: <c>/customer</c>.</summary>
public sealed class Customers
{
    private readonly ApiClient _api;

    internal Customers(ApiClient api) => _api = api;

    /// <summary><c>POST /customer/card</c></summary>
    public Task<CustomerData> CreateWithCardAsync(NewCustomerCard customer, CancellationToken cancellationToken = default) =>
        _api.PostAsync<CustomerData>("/customer/card", customer, cancellationToken);

    /// <summary><c>POST /customer/token</c></summary>
    public Task<CustomerData> CreateWithTokenAsync(NewCustomerToken customer, CancellationToken cancellationToken = default) =>
        _api.PostAsync<CustomerData>("/customer/token", customer, cancellationToken);

    /// <summary><c>POST /customer/payto</c></summary>
    public Task<CustomerData> CreateWithPayToAsync(NewCustomerPayTo customer, CancellationToken cancellationToken = default) =>
        _api.PostAsync<CustomerData>("/customer/payto", customer, cancellationToken);

    /// <summary><c>POST /customer/provider_token</c>: a payment method the gateway already stores.</summary>
    public Task<CustomerData> CreateWithProviderTokenAsync(NewCustomerProviderToken customer, CancellationToken cancellationToken = default) =>
        _api.PostAsync<CustomerData>("/customer/provider_token", customer, cancellationToken);

    /// <summary><c>POST /customer/provider_single_use_token</c></summary>
    public Task<CustomerData> CreateWithSingleUseTokenAsync(SingleUseToken customer, CancellationToken cancellationToken = default) =>
        _api.PostAsync<CustomerData>("/customer/provider_single_use_token", customer, cancellationToken);

    /// <summary><c>POST /customer/bank_account</c></summary>
    public Task<CustomerData> CreateWithBankAccountAsync(NewCustomerBankPayment customer, CancellationToken cancellationToken = default) =>
        _api.PostAsync<CustomerData>("/customer/bank_account", customer, cancellationToken);

    /// <summary><c>GET /customer/{customerId}</c></summary>
    public Task<CustomerData> GetAsync(string customerId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<CustomerData>("/customer/" + ApiClient.Id(customerId, nameof(customerId)), null, cancellationToken);

    /// <summary><c>GET /customer</c></summary>
    public Task<CustomerList> SearchAsync(CustomerSearch? search = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<CustomerList>("/customer", search?.ToQuery(), cancellationToken);

    /// <summary><c>PUT /customer/{customerId}</c></summary>
    public Task<CustomerData> UpdateAsync(string customerId, UpdateCustomer customer, CancellationToken cancellationToken = default) =>
        _api.PutAsync<CustomerData>("/customer/" + ApiClient.Id(customerId, nameof(customerId)), customer, cancellationToken);

    /// <summary><c>DELETE /customer/{customerId}</c>: removes the customer and its payment methods, and with <paramref name="gatewayDelete"/> also at the gateway.</summary>
    public Task<CustomerData> DeleteAsync(string customerId, bool? gatewayDelete = null, CancellationToken cancellationToken = default) =>
        _api.DeleteAsync<CustomerData>("/customer/" + ApiClient.Id(customerId, nameof(customerId)), Query.Of(("gatewayDelete", gatewayDelete)), cancellationToken);

    /// <summary><c>GET /customer/{customerId}/payment_method</c></summary>
    public Task<List<PaymentMethodData>> GetPaymentMethodsAsync(string customerId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<List<PaymentMethodData>>("/customer/" + ApiClient.Id(customerId, nameof(customerId)) + "/payment_method", null, cancellationToken);

    /// <summary><c>POST /customer/{customerId}/payment_method/card</c></summary>
    public Task<PaymentMethodData> AddPaymentMethodWithCardAsync(string customerId, NewPaymentMethodCard paymentMethod, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentMethodData>("/customer/" + ApiClient.Id(customerId, nameof(customerId)) + "/payment_method/card", paymentMethod, cancellationToken);

    /// <summary><c>POST /customer/{customerId}/payment_method/token</c></summary>
    public Task<PaymentMethodData> AddPaymentMethodWithTokenAsync(string customerId, NewPaymentMethodToken paymentMethod, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentMethodData>("/customer/" + ApiClient.Id(customerId, nameof(customerId)) + "/payment_method/token", paymentMethod, cancellationToken);

    /// <summary><c>POST /customer/{customerId}/payment_method/payto</c></summary>
    public Task<PaymentMethodData> AddPaymentMethodWithPayToAsync(string customerId, NewPayToAgreement agreement, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentMethodData>("/customer/" + ApiClient.Id(customerId, nameof(customerId)) + "/payment_method/payto", agreement, cancellationToken);

    /// <summary><c>POST /customer/{customerId}/bank_account</c></summary>
    public Task<PaymentMethodData> AddPaymentMethodWithBankAccountAsync(string customerId, NewPaymentMethodBankPayment paymentMethod, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentMethodData>("/customer/" + ApiClient.Id(customerId, nameof(customerId)) + "/bank_account", paymentMethod, cancellationToken);
}
