using BlazorShoppingCart.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly HalloweenDBContext halloweenDBContext;

        public CustomersRepository(HalloweenDBContext halloweenDBContext)
        {
            this.halloweenDBContext = halloweenDBContext;
        }

        public async Task<Customers> AddCustomer(Customers customer)
        {
           await halloweenDBContext.Customers.AddAsync(customer);
           await halloweenDBContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customers> DeleteCustomer(string email)
        {
            var customer = await halloweenDBContext.Customers.Include(c => c.StateNavigation).FirstOrDefaultAsync(c => c.Email == email);
            if (customer != null)
            {
                halloweenDBContext.Remove(customer);
                await halloweenDBContext.SaveChangesAsync();
                return customer;
            }
            return null;
        }

        public async Task<IList<Customers>> GetAllCustomers()
        {
            return await halloweenDBContext.Customers.Include(c => c.StateNavigation).ToListAsync();
        }

        public async Task<Customers> GetCustomer(string email)
        {
            return await halloweenDBContext.Customers.Include(c => c.StateNavigation).FirstOrDefaultAsync(c => c.Email == email); 
        }

        public async Task<Customers> UpdateCustomer(Customers customer)
        {
           var result = await halloweenDBContext.Customers.Include(c => c.StateNavigation).FirstOrDefaultAsync(c => c.Email == customer.Email);
            if (result != null)
            {
                result.LastName = customer.LastName;
                result.FirstName = customer.FirstName;
                result.Address = customer.Address;
                result.City = customer.City;
                result.State = customer.State;
                result.PhoneNumber = customer.PhoneNumber;

                return result;
            }
            return null;
        }
    }
}
