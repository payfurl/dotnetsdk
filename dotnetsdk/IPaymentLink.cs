using System.Threading.Tasks;
using payfurl.sdk.Models.PaymentLink;

namespace payfurl.sdk
{
    public interface IPaymentLink
    {
        Task<PaymentLinkData> CreateAsync(CreatePaymentLink paymentLink);
        PaymentLinkData Create(CreatePaymentLink paymentLink);
        Task<PaymentLinkData> SingleAsync(string paymentLinkId);
        PaymentLinkData Single(string paymentLinkId);
        Task<SearchPaymentLinkResult> SearchAsync(SearchPaymentLink search);
        SearchPaymentLinkResult Search(SearchPaymentLink search);
    }
}