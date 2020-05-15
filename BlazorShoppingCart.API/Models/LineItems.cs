using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorShoppingCart.API.Models
{
    public partial class LineItems
    {
        [Required, Display(Name = "Invoice Number")]
        public int InvoiceNumber { get; set; }
        [Required, Display(Name = "Product ID")]
        public string ProductId { get; set; }
        [Required, Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal? Extension { get; set; }

        public virtual Invoices InvoiceNumberNavigation { get; set; }
        public virtual Products Product { get; set; }
    }
}
