using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorShoppingCart.API.Models
{
    public partial class States
    {
        public States()
        {
            Customers = new HashSet<Customers>();
        }

        [Required, Display(Name = "State Code")]
        public string StateCode { get; set; }
        [Required, Display(Name = "State Name")]
        public string StateName { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
    }
}
