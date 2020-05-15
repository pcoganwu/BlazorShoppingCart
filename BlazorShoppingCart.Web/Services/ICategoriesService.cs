using BlazorShoppingCart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.Web.Services
{
    public interface ICategoriesService
    {
        Task<IList<Categories>> GetCategories();
    }
}
