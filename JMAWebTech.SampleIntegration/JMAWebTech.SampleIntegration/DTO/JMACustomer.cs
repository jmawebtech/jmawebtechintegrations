using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMA.Plugin.Accounting.QuickBooks.DTO
{
    public class JMACustomer
    {
        public bool IsTaxExempt { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
    }
}
