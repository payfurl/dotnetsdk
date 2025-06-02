using payfurl.sdk.Models;
using System.Threading.Tasks;
using payfurl.sdk.Models.Token;
using Xunit;

namespace FunctionalTests
{
    public class Token : BaseTest
    {
        [Fact]
        public void Single()
        {
            var svc = new payfurl.sdk.Token();
            var tokenId = GetPaymentToken();

            var token = svc.Single(tokenId);

            Assert.Equal(tokenId, token.TokenId);
        }

        [Fact]
        public async Task SingleAsync()
        {
            var svc = new payfurl.sdk.Token();
            var tokenId = GetPaymentToken();

            var token = await svc.SingleAsync(tokenId);

            Assert.Equal(tokenId, token.TokenId);
        }

        [Fact]
        public void Search()
        {
            var svc = new payfurl.sdk.Token();
            var tokenSearch = new TokenSearch
            {
                Limit = 10
            };

            var tokenList = svc.Search(tokenSearch);

            Assert.Equal(tokenSearch.Limit, tokenList.Limit);
        }

        [Fact]
        public async Task SearchAsync()
        {
            var svc = new payfurl.sdk.Token();
            var tokenSearch = new TokenSearch
            {
                Limit = 10
            };

            var tokenList = await svc.SearchAsync(tokenSearch);

            Assert.Equal(tokenSearch.Limit, tokenList.Limit);
        }
        
        [Fact]
        public void TokenisePaymentMethod()
        {
            var paymentMethodService = new payfurl.sdk.PaymentMethod();
            var newPaymentMethod = new NewPaymentMethodCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = GetPaymentInformation()
            }; 
            var paymentMethod = paymentMethodService.CreatePaymentMethodWithCard(newPaymentMethod);
            Assert.NotNull(paymentMethod.PaymentMethodId);

            var svc = new payfurl.sdk.Token();
            var newTokenPaymentMethod = new NewTokenPaymentMethod
            {
                PaymentMethodId = paymentMethod.PaymentMethodId,
            };

            var tokenData = svc.TokenisePaymentMethod(newTokenPaymentMethod);

            Assert.NotNull(tokenData.TokenId);
        }

        [Fact]
        public void TokeniseCard()
        {
            var svc = new payfurl.sdk.Token();
            var newTokenCard = new NewTokenCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = GetPaymentInformation()
            };

            var tokenData = svc.TokeniseCard(newTokenCard);

            Assert.NotNull(tokenData.TokenId);
        }
        
        [Fact]
        public void TokeniseCardLeastCost()
        {
            var svc = new payfurl.sdk.Token();
            var newTokenCard = new NewTokenCard
            {
                PaymentInformation = GetPaymentInformation(),
            };

            var tokenData = svc.TokeniseCardLeastCost(newTokenCard);

            Assert.NotNull(tokenData.TokenId);
        }
        
        private static CardRequestInformation GetPaymentInformation()
        {
            return new CardRequestInformation
            {
                CardNumber = "4111111111111111",
                ExpiryDate = "12/35",
                Ccv = "123"
            };
        }
    }
}
