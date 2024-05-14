using System;

namespace payfurl.sdk.Models.Batch
{
    public class BatchData
    {
        public string BatchId { get; set; } 
        public int Count { get; set; } 
        public string Description { get; set; } 
        public string Status { get; set; } 
        public decimal Progress { get; set; } 
        public string Results { get; set; }
        public DateTime DateAdded { get; set; }
    }
}