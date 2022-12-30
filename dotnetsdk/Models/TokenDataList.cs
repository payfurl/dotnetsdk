using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class TokenDataList
    {
        public int Limit { get; set; }
        public int Skip { get; set; }
        public long Count { get; set; }
        public List<TokenData> Tokens { get; set; }
    }
}