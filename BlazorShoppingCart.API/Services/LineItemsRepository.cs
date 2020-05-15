using BlazorShoppingCart.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public class LineItemsRepository : ILineItemsRepository
    {
        private readonly HalloweenDBContext halloweenDBContext;

        public LineItemsRepository(HalloweenDBContext halloweenDBContext)
        {
            this.halloweenDBContext = halloweenDBContext;
        }

        public async Task<LineItems> AddLineItem(LineItems lineItem)
        {
            var result = await halloweenDBContext.LineItems.AddAsync(lineItem);
            await halloweenDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<LineItems> DeleteLineItem(int invoiceNumber)
        {
            var lineItem = halloweenDBContext.LineItems
                .Include(i => i.InvoiceNumberNavigation).Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);

            if (lineItem != null)
            {
                halloweenDBContext.Remove(lineItem);
                await halloweenDBContext.SaveChangesAsync();
                return await lineItem;
            }
            return null;
        }

        public async Task<IList<LineItems>> GetAllLineItems()
        {
            return await halloweenDBContext.LineItems
                .Include(i => i.InvoiceNumberNavigation).Include(i => i.Product).ToListAsync();
        }

        public async Task<LineItems> GetLineItem(int invoiceNumber)
        {
            return await halloweenDBContext.LineItems
                .Include(i => i.InvoiceNumberNavigation).Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);
        }

        public async Task<LineItems> UpdateLineItem(LineItems lineItem)
        {
            var result = await halloweenDBContext.LineItems
                .Include(i => i.InvoiceNumberNavigation).Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.InvoiceNumber == lineItem.InvoiceNumber);

            if (result != null)
            {
                result.ProductId = lineItem.ProductId;
                result.UnitPrice = lineItem.UnitPrice;
                result.Extension = lineItem.Extension;

                return result;
            }
            return null;
        }
    }
}
