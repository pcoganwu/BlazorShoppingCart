using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorShoppingCart.API.Models;
using BlazorShoppingCart.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoicesRepository invoicesRepository;

        public InvoicesController(IInvoicesRepository invoicesRepository)
        {
            this.invoicesRepository = invoicesRepository;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<Invoices>> GetInvoices()
        {
            try
            {
                return Ok(await invoicesRepository.GetAllInvoices());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        // GET: api/Invoices/5
        [HttpGet("{invoiceNumber}")]
        public async Task<ActionResult<Invoices>> GetInvoice(int invoiceNumber)
        {
            try
            {
                var invoice = await invoicesRepository.GetInvoice(invoiceNumber);

                if (invoice == null)
                {
                    return NotFound($"Invoice with number {invoiceNumber} cannopt be found");
                }

                return invoice;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        // POST: api/Invoices
        [HttpPost]
        public async Task<ActionResult<Invoices>> CreateInvoice([FromBody] Invoices invoice)
        {
            try
            {
                if (invoice == null)
                {
                    return BadRequest();
                }

                var invoiceToCreate = await invoicesRepository.AddInvoice(invoice);

                return CreatedAtAction(nameof(GetInvoice), new { invoiceNumber = invoiceToCreate.InvoiceNumber }, invoiceToCreate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving creating a new invoice");
            }
        }

        // PUT: api/Invoices/5
        [HttpPut("{invoiceNumber}")]
        public async Task<ActionResult<Invoices>> UpdateInvoice(int invoiceNumber, [FromBody] Invoices invoice)
        {
            try
            {
                if (invoiceNumber != invoice.InvoiceNumber)
                {
                    return BadRequest("Invoice number mismatch");
                }

                var invoiceToUpdate = await invoicesRepository.GetInvoice(invoiceNumber);

                if (invoiceToUpdate == null)
                {
                    return NotFound($"Invoice with number = {invoiceNumber} cannot be found");
                }

                return await invoicesRepository.UpdateInvoice(invoice);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating invoice");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{invoiceNumber}")]
        public async Task<ActionResult<Invoices>> DeleteInvoice(int invoiceNumber)
        {
            try
            {
                var invoiceToDelete = await invoicesRepository.GetInvoice(invoiceNumber);

                if (invoiceToDelete == null)
                {
                    return NotFound($"Invoice with number = {invoiceNumber} cannot be found");
                }

                return await invoicesRepository.DeleteInvoice(invoiceNumber);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting invoice");
            }
        }
    }
}
