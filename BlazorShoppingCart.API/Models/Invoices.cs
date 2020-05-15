using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorShoppingCart.API.Models
{
    public partial class Invoices
    {
        public Invoices()
        {
            LineItems = new HashSet<LineItems>();
        }

        [Required, Display(Name = "Invoice Number")]
        public int InvoiceNumber { get; set; }
        [Required, Display(Name = "Email")]
        public string CustEmail { get; set; }
        [Display(Name = "Order Data")]
        public DateTime OrderDate { get; set; }
        [Required]
        public decimal Subtotal { get; set; }
        [Required, Display(Name = "Ship Method")]
        public string ShipMethod { get; set; }
        [Required]
        public decimal Shipping { get; set; }
        [Required, Display(Name = "Sales Tax")]
        public decimal SalesTax { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Display(Name = "Credit Card Type")]
        public string CreditCardType { get; set; }
        [Required, Display(Name = "Card Number")]
        public string CardNumber { get; set; }
        [Required, Display(Name = "Expiration Month")]
        public short ExpirationMonth { get; set; }
        [Required, Display(Name = "Expiration Year")]
        public short ExpirationYear { get; set; }

        public virtual Customers CustEmailNavigation { get; set; }
        public virtual ICollection<LineItems> LineItems { get; set; }
    }
}
