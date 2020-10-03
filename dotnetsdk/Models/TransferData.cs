using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace payfurl.sdk.Models
{
    public class TransferData
    {
        public string TransferId { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public List<TransferItem> TransferItems { get; set; }

        public class TransferItem
        {
            public decimal Amount { get; set; }
            public string Currency { get; set; }
            public string Account { get; set; }
        }
    }
}
