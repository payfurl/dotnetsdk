using System;

namespace evertech.sdk.Models
{
    public class PaymentMethod
    {
        public string Id { get; set; }
        public CardData Card { get; set; }
        public string Type { get; set; }
        public string ProviderType { get; set; }
        public DateTime DateAdded { get; set; }
        public string CustomerId { get; set; }
        public string Email { get; set; }
    }
}
