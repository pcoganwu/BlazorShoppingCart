using BlazorShoppingCart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public interface IProductsRepository
    {
        Task<IList<Products>> GetAllProducts();
        Task<Products> GetProduct(string productId);
        Task<Products> AddProduct(Products product);
        Task<Products> UpdateProduct(Products product);
        Task<Products> DeleteProduct(string productId);
    }
}
