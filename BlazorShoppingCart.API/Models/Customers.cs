using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorShoppingCart.API.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Invoices = new HashSet<Invoices>();
        }

        public string Email { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
        [Required, Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public virtual States StateNavigation { get; set; }
        public virtual ICollection<Invoices> Invoices { get; set; }
    }
}
