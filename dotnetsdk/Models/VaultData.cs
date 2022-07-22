using System;

namespace payfurl.sdk.Models
{
    public class VaultData
    {
        public string VaultId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string MaskedCardNumber { get; set; }
        public bool HasCvv { get; set; }
    }
}