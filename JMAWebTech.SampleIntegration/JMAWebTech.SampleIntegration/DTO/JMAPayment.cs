using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMA.Plugin.Accounting.QuickBooks.DTO
{
    public class JMAPayment
    {
        public int OrderId { get; set; }
        public string CreditCardName { get; set; }
        public string PaymentType { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
