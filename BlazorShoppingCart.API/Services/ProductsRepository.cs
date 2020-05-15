using BlazorShoppingCart.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly HalloweenDBContext halloweenDBContext;

        public ProductsRepository(HalloweenDBContext halloweenDBContext)
        {
            this.halloweenDBContext = halloweenDBContext;
        }
        public async Task<Products> AddProduct(Products product)
        {
            var result = await halloweenDBContext.Products.AddAsync(product);
            await halloweenDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Products> DeleteProduct(string productId)
        {
            var product = await halloweenDBContext.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product != null)
            {
                halloweenDBContext.Remove(product);
                await halloweenDBContext.SaveChangesAsync();
                return product;
            }
            return null;
        }

        public async Task<IList<Products>> GetAllProducts()
        {
            return await halloweenDBContext.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Products> GetProduct(string productId)
        {
            return await halloweenDBContext.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<Products> UpdateProduct(Products product)
        {
            var productToUpdate = await halloweenDBContext.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.ShortDescription = product.ShortDescription;
                productToUpdate.LongDescription = product.LongDescription;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.ImageFile = product.ImageFile;
                productToUpdate.UnitPrice = product.UnitPrice;
                productToUpdate.OnHand = product.OnHand;

                await halloweenDBContext.SaveChangesAsync();

                return productToUpdate;
            }
            return null;
        }
    }
}
