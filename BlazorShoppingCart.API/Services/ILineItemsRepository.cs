using BlazorShoppingCart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public interface ILineItemsRepository
    {
        Task<IList<LineItems>> GetAllLineItems();
        Task<LineItems> GetLineItem(int invoiceNumber);
        Task<LineItems> AddLineItem(LineItems lineItem);
        Task<LineItems> UpdateLineItem(LineItems lineItem);
        Task<LineItems> DeleteLineItem(int invoiceNumber);
    }
}
