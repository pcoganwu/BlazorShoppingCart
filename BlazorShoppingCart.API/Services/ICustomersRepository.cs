using BlazorShoppingCart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public interface ICustomersRepository
    {
        Task<IList<Customers>> GetAllCustomers();
        Task<Customers> GetCustomer(string email);
        Task<Customers> AddCustomer(Customers customer);
        Task<Customers> UpdateCustomer(Customers customer);
        Task<Customers> DeleteCustomer(string email);
    }
}
