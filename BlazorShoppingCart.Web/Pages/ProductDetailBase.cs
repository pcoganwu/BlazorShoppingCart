using BlazorShoppingCart.API.Models;
using BlazorShoppingCart.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.Web.Pages
{
    public class ProductDetailBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        public Products Product { get; set; } = new Products();

        [Parameter]
        public string ProductId { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Product = await ProductService.GetProduct(ProductId);
        }
    }
}
