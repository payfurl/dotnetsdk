using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class NewTransfer
    {
        public string ProviderId { get; set; }
        public string Reference { get; set; }
        public List<TransferItem> Transfers { get; set; }
    }
}
