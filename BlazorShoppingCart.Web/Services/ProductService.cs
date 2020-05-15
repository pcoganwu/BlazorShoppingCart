using BlazorShoppingCart.API.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorShoppingCart.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Products> GetProduct(string productId)
        {
            return await httpClient.GetJsonAsync<Products>($"api/products/{productId}");
        }

        public async Task<IList<Products>> GetProducts()
        {
            return await httpClient.GetJsonAsync<List<Products>>("api/products");
        }

        public async Task<Products> UpdateProduct(Products updatedProduct)
        {
            return await httpClient.PutJsonAsync<Products>("api/products", updatedProduct); ;
        }
    }
}
