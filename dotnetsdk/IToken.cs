using payfurl.sdk.Models;
using System.Threading.Tasks;

namespace payfurl.sdk
{
    public interface IToken
    {
        TokenData Single(string tokenId);
        Task<TokenData> SingleAsync(string tokenId);
        TokenDataList Search(TokenSearch searchData);
        Task<TokenDataList> SearchAsync(TokenSearch searchData);
    }
}