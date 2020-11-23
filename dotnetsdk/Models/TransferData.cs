using System;

namespace payfurl.sdk.Models
{
    public class TransferData
    {
        public string TransferId { get; set; }
        public string Status { get; set; }
        public string Reference { get; set; }
        public string GroupReference { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProviderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Account { get; set; }
    }
}
