using BlazorShoppingCart.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly HalloweenDBContext halloweenDBContext;

        public InvoicesRepository(HalloweenDBContext halloweenDBContext)
        {
            this.halloweenDBContext = halloweenDBContext;
        }

        public async Task<Invoices> AddInvoice(Invoices invoice)
        {
            var result = await halloweenDBContext.Invoices.AddAsync(invoice);
            await halloweenDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Invoices> DeleteInvoice(int invoiceNumber)
        {
           var result = await halloweenDBContext.Invoices.Include(i => i.CustEmailNavigation)
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);
            if (result != null)
            {
                halloweenDBContext.Remove(result);
                await halloweenDBContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IList<Invoices>> GetAllInvoices()
        {
            return await halloweenDBContext.Invoices.Include(i => i.CustEmailNavigation).ToListAsync();
        }

        public async Task<Invoices> GetInvoice(int invoiceNumber)
        {
            return await halloweenDBContext.Invoices.Include(i => i.CustEmailNavigation)
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);
        }

        public async Task<Invoices> UpdateInvoice(Invoices invoice)
        {
            var result = await halloweenDBContext.Invoices.Include(i => i.CustEmailNavigation)
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoice.InvoiceNumber);
            if (result != null)
            {
                result.CustEmail = invoice.CustEmail;
                result.OrderDate = invoice.OrderDate;
                result.Subtotal = invoice.Subtotal;
                result.ShipMethod = invoice.ShipMethod;
                result.Shipping = invoice.Shipping;
                result.SalesTax = invoice.SalesTax;
                result.Total = invoice.Total;
                result.CreditCardType = invoice.CreditCardType;
                result.CardNumber = invoice.CardNumber;
                result.ExpirationMonth = invoice.ExpirationMonth;
                result.ExpirationYear = invoice.ExpirationYear;

                return result;
            }
            return null;
        }
    }
}
