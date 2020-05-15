using BlazorShoppingCart.API.Models;
using BlazorShoppingCart.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.Web.Pages
{
    public class EditProductBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public ICategoriesService CategoryService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string ProductId { get; set; }

        public IFormFile Photo { get; set; }

        public Products Product { get; set; } = new Products();

        public IList<Categories> Categories { get; set; } = new List<Categories>();

        protected async override Task OnInitializedAsync()
        {
            Categories = await CategoryService.GetCategories();

            Product =  await ProductService.GetProduct(ProductId);
        }

        protected async Task HandleValidSubmit()
        {
           var result = await ProductService.UpdateProduct(Product);

           if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
