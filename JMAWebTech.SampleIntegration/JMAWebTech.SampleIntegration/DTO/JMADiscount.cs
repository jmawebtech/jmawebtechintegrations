using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JMA.Plugin.Accounting.QuickBooks.DTO
{
    public class JMADiscount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal Percent { get; set; }
        public string Code { get; set; }
        //public JMAOrder Order { get; set; }
    }
}
