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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository customersRepository;

        public CustomersController(ICustomersRepository customersRepository)
        {
            this.customersRepository = customersRepository;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<Customers>> GetCustomers()
        {
            try
            {
                return Ok(await customersRepository.GetAllCustomers());
            }
            catch (Exception)
            {

              return  StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        // GET: api/Customers/5
        [HttpGet("{email}")]
        public async Task<ActionResult<Customers>> GetCustomer(string email)
        {
            try
            {
                var customer = await customersRepository.GetCustomer(email);

                if (customer == null)
                {
                    return NotFound();
                }

                return customer;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customers>> CreateCustomer([FromBody] Customers customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }

                var customerToCreate = await customersRepository.AddCustomer(customer);

                return CreatedAtAction(nameof(GetCustomer), new { email = customerToCreate.Email }, customerToCreate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving creating a new customer");
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{email}")]
        public async Task<ActionResult<Customers>> UpdateCustomer(string email, [FromBody] Customers customer)
        {
            try
            {
                if (email != customer.Email)
                {
                    return BadRequest("Email mismatch");
                }

                var customerToUpdate = await customersRepository.GetCustomer(email); 

                if (customerToUpdate == null)
                {
                    return NotFound($"Custmer with email = {email} cannot be found");
                }

                return await customersRepository.UpdateCustomer(customer);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating customer");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{email}")]
        public async Task<ActionResult<Customers>> DeleteCustomer(string email)
        {
            try
            {
                var customerToDelete = await customersRepository.GetCustomer(email);

                if (customerToDelete == null)
                {
                    return NotFound($"Custmer with email = {email} cannot be found");
                }

                return await customersRepository.DeleteCustomer(email);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting customer");
            }
        }
    }
}
