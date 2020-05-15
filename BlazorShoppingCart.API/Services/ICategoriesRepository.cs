using BlazorShoppingCart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public interface ICategoriesRepository
    {
        Task<IList<Categories>> GetAllCategories();
        Task<Categories> GetCategory(string categoryId);
        Task<Categories> AddCategory(Categories category);
        Task<Categories> UpdateCategory(Categories category);
        Task<Categories> DeleteCategory(string categoryId);
    }
}
