using System.Collections.Generic;

namespace payfurl.sdk.Models
{
    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
    }

    public class Order
    {
        public string OrderNumber { get; set; }
        public decimal FreightAmount { get; set; }
        public decimal DutyAmount { get; set; }
        public List<ProductItem> Items { get; set; }
    }

    public class ProductItem
    {
        public string ProductCode { get; set; }
        public string CommodityCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
