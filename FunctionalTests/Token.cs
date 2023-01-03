using payfurl.sdk;
using payfurl.sdk.Models;
using System.Threading.Tasks;
using Xunit;
using Environment = payfurl.sdk.Environment;

namespace FunctionalTests
{
    public class Token
    {
        public Token()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Fact(Skip = "tokens expire, so this test needs to be adjusted each time it's run")]
        public void Single()
        {
            var svc = new payfurl.sdk.Token();
            var tokenId = "b9a28ccf5ff148d09a719903b9901142";

            var token = svc.Single(tokenId);

            Assert.Equal(tokenId, token.TokenId);
        }

        [Fact(Skip = "tokens expire, so this test needs to be adjusted each time it's run")]
        public async Task SingleAsync()
        {
            var svc = new payfurl.sdk.Token();
            var tokenId = "b9a28ccf5ff148d09a719903b9901142";

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
    }
}
