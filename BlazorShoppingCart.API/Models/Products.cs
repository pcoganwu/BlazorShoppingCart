using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorShoppingCart.API.Models
{
    public partial class Products
    {
        public Products()
        {
            LineItems = new HashSet<LineItems>();
        }

        [Display(Name = "Product ID")]
        public string ProductId { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required, Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        [Required, Display(Name = "Long Description")]
        public string LongDescription { get; set; }
        [Required, Display(Name = "Category ID")]
        public string CategoryId { get; set; }
        [Display(Name = "Image File")]
        public string ImageFile { get; set; }
        [Required, Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        [Required, Display(Name = "On Hand")]
        public int OnHand { get; set; }

        public virtual Categories Category { get; set; }
        public virtual ICollection<LineItems> LineItems { get; set; }
    }
}
