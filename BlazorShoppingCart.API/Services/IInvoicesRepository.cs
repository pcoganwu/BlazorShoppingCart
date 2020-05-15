using BlazorShoppingCart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public interface IInvoicesRepository
    {
        Task<IList<Invoices>> GetAllInvoices();
        Task<Invoices> GetInvoice(int invoiceNumber);
        Task<Invoices> AddInvoice(Invoices invoice);
        Task<Invoices> UpdateInvoice(Invoices invoice);
        Task<Invoices> DeleteInvoice(int invoiceNumber);
    }
}
