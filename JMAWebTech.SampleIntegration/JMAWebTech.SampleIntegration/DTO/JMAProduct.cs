using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JMA.Plugin.Accounting.QuickBooks.DTO
{
    public class JMAProduct
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public decimal PurchaseCost { get; set; }
        public int StockQuantity { get; set; }
        public bool IsDownload { get; set; }
        public string Vendor { get; set; }
        public string Department { get; set; }
        public List<JMAManufacturer> Manufacturers { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string Description { get; set; }
    }
}
