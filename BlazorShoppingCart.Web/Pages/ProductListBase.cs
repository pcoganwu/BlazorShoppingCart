using BlazorShoppingCart.API.Models;
using BlazorShoppingCart.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorShoppingCart.Web.Pages
{
    public class ProductListBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public ICategoriesService CategoriesService { get; set; }

        public IList<Categories> Categories { get; set; } = new List<Categories>();

        //public Categories Category { get; set; } = new Categories();

        public IList<Products> Products { get; set; } = new List<Products>();

        public string CategoryId { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Categories = await CategoriesService.GetCategories();

            Products = await ProductService.GetProducts(); 
        }

        private string category;

        public string Category
        {
            get
            {
                return category;
            }
            set 
            {
                category = value;
                GetProducts();
            }
        }

        public async Task GetProducts()
        {
           Products = (await ProductService.GetProducts()).Where(p => p.CategoryId == category).ToList();
        }
    }
}
