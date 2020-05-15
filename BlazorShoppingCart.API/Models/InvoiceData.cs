using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorShoppingCart.API.Models
{
    public partial class InvoiceData
    {
        [Required]
        public decimal SalesTax { get; set; }
    }
}
