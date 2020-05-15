using BlazorShoppingCart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.Web.Services
{
    public interface IProductService
    {
        Task<IList<Products>> GetProducts();
        Task<Products> GetProduct(string productId);

        Task<Products> UpdateProduct(Products updatedProduct);
    }
}
