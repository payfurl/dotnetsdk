using System.Collections.Generic;

namespace payfurl.sdk.Models
{    public class TransferList
    {
        public int Limit { get; set; }
        public int Skip { get; set; }
        public int Count { get; set; }
        public List<TransferData> Transfers { get; set; }
    }
}
