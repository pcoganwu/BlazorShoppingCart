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
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository productsRepository;
        private readonly HalloweenDBContext halloweenDBContext;

        public ProductsController(IProductsRepository productsRepository, HalloweenDBContext halloweenDBContext)
        {
            this.productsRepository = productsRepository;
            this.halloweenDBContext = halloweenDBContext;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<Products>> GetProducts()
        {
            try
            {
                return Ok(await productsRepository.GetAllProducts());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }

        }

        // GET: api/Products/5
        [HttpGet("{productId}")]
        public async Task<ActionResult<Products>> GetProduct(string productId)
        {
            try
            {
                var result = await productsRepository.GetProduct(productId);

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

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Products>> CreateProduct([FromBody] Products product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }

               var createdProduct = await productsRepository.AddProduct(product);

                return CreatedAtAction(nameof(GetProduct), new { productId = createdProduct.ProductId}, createdProduct);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data");
            }
        }

        // PUT: api/Products/5
        [HttpPut()]
        public async Task<ActionResult<Products>> UpdateProduct([FromBody] Products product)
        {
            try
            {
                //if (productId != product.ProductId)
                //{
                //    return BadRequest("Product ID mismatch");
                //}

                var productToUpdate = await productsRepository.GetProduct(product.ProductId);

                if (productToUpdate == null)
                {
                    return NotFound($"Product with ProductId = {product.ProductId} cannot be found");
                }

                return await productsRepository.UpdateProduct(product);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{productId}")]
        public async Task<ActionResult<Products>> Delete(string productId)
        {
            try
            {
                var productToDelete = await productsRepository.GetProduct(productId);

                if (productToDelete == null)
                {
                    return NotFound($"Product with ProductId = {productId} cannot be found");
                }

                return await productsRepository.DeleteProduct(productId);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
