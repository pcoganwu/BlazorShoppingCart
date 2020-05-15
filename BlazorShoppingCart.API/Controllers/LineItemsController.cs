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
    public class LineItemsController : ControllerBase
    {
        private readonly ILineItemsRepository lineItemsRepository;

        public LineItemsController(ILineItemsRepository lineItemsRepository)
        {
            this.lineItemsRepository = lineItemsRepository;
        }

        // GET: api/LineItems
        [HttpGet]
        public async Task<ActionResult<LineItems>> GetLineItems()
        {
            try
            {
                return Ok(await lineItemsRepository.GetAllLineItems());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }

        }

        // GET: api/LineItems/5
        [HttpGet("{invoiceNumber}")]
        public async Task<ActionResult<LineItems>> GetLineItem(int invoiceNumber)
        {
            try
            {
                var result = await lineItemsRepository.GetLineItem(invoiceNumber);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            };
        }

        // POST: api/LineItems
        [HttpPost]
        public async Task<ActionResult<LineItems>> CreateLineItem([FromBody] LineItems lineItem)
        {
            try
            {
                if (lineItem == null)
                {
                    return BadRequest();
                }

                var createdLineItem = await lineItemsRepository.AddLineItem(lineItem);

                return CreatedAtAction(nameof(GetLineItem), new { invoiceNumber = createdLineItem.InvoiceNumber }, createdLineItem);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data");
            }
        }

        // PUT: api/LineItems/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
