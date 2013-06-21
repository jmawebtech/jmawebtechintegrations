using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JMA.Plugin.Accounting.QuickBooks.DTO
{
    public class JMAOrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        //public JMAOrder Order { get; set; }
        public JMAProduct Product { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal PriceExclTax { get; set; }
        public decimal PriceInclTax { get; set; }
        public decimal DiscountExclTax { get; set; }
        public decimal DiscountInclTax { get; set; }
        public string Sku { get; set; }
        public bool HasTax { get; set; }
        public string UnitOfMeasure { get; set; }
    }
}
