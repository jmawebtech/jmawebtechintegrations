using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JMA.Plugin.Accounting.QuickBooks.DTO
{
    public class JMAOrder
    {
        public int Id { get; set; }
        public decimal TotalExclTax { get; set; }
        public decimal TotalInclTax { get; set; }
        public decimal DiscountExclTax { get; set; }
        public decimal DiscountInclTax { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public string Note { get; set; }
        public List<JMAOrderDetail> OrderDetails { get; set; }
        public decimal Total { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TaxRate { get; set; }
        public JMAAddress ShippingAddress { get; set; }
        public JMAAddress BillingAddress { get; set; }
        public decimal PaymentMethodAdditionalFeeExclTax { get; set; }
        public decimal ShippingExclTax { get; set; }
        public decimal ShippingInclTax { get; set; }
        public List<JMADiscount> Discounts = new List<JMADiscount>();
        public string ShippingMethod { get; set; }
        public string CardType { get; set; }
        public string CreditCardName { get; set; }
        public JMACustomer Customer { get; set; }
        public List<JMAPayment> Payments { get; set; }
    }
}
