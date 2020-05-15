using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorShoppingCart.API.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public string CategoryId { get; set; }
        [Required, Display(Name = "Short Name")]
        public string ShortName { get; set; }
        [Required, Display(Name = "Long Name")]
        public string LongName { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
