using BlazorShoppingCart.API.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorShoppingCart.Web.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient httpClient;

        public CategoriesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IList<Categories>> GetCategories()
        {
            return await httpClient.GetJsonAsync<List<Categories>>("api/categories");
        }
    }
}
