using BlazorShoppingCart.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly HalloweenDBContext halloweenDBContext;

        public CategoriesRepository(HalloweenDBContext halloweenDBContext)
        {
            this.halloweenDBContext = halloweenDBContext;
        }
        public async Task<Categories> AddCategory(Categories category)
        {
            var result = await halloweenDBContext.Categories.AddAsync(category);

            await halloweenDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Categories> DeleteCategory(string categoryId)
        {
            var category = await halloweenDBContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            if (category != null)
            {
                halloweenDBContext.Remove(category);
                await halloweenDBContext.SaveChangesAsync();
                return category;
            }
            return null;
        }

        public async Task<IList<Categories>> GetAllCategories()
        {
            return await halloweenDBContext.Categories.ToListAsync();
        }

        public async Task<Categories> GetCategory(string categoryId)
        {
            return await halloweenDBContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<Categories> UpdateCategory(Categories category)
        {
            var result = await halloweenDBContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);
            if (result != null)
            {
                result.ShortName = category.ShortName;
                result.LongName = category.LongName;
                return result;
            }
            return null;
        }
    }
}
