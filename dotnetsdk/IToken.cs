using payfurl.sdk.Models;
using System.Threading.Tasks;
using payfurl.sdk.Models.Token;

namespace payfurl.sdk
{
    public interface IToken
    {
        TokenData Single(string tokenId);
        Task<TokenData> SingleAsync(string tokenId);
        TokenDataList Search(TokenSearch searchData);
        Task<TokenDataList> SearchAsync(TokenSearch searchData);
        TokenData TokenisePaymentMethod(NewTokenPaymentMethod newTokenPaymentMethod);
        Task<TokenData> TokenisePaymentMethodAsync(NewTokenPaymentMethod newTokenPaymentMethod);
        TokenData TokeniseCard(NewTokenCard newTokenCard);
        Task<TokenData> TokeniseCardAsync(NewTokenCard newTokenCard);
        TokenData TokeniseCardLeastCost(NewTokenCard newTokenCardLeastCost);
        Task<TokenData> TokeniseCardLeastCostAsync(NewTokenCard newTokenCardLeastCost);
    }
}