using System;

namespace payfurl.sdk.Models
{
    public class NewVault
    { 
        public string CardNumber { get; set; }
        public string Ccv { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int? ExpireSeconds { get; set; }
    }
}