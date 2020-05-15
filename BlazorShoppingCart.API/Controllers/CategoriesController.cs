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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<Categories>> GetCategories()
        {
            try
            {
                return Ok(await categoriesRepository.GetAllCategories());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Category data");
            }
        }

        // GET: api/Categories/5
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<Categories>> GetCategory(string categoryId)
        {
            try
            {
                var result = await categoriesRepository.GetCategory(categoryId);

                if (result == null)
                {
                    return NotFound($"Category with Category ID = {categoryId} cannot be found");
                }

                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Category data");
            }
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Categories>> CreateCategory([FromBody] Categories category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }

                var categoryToCreate = await categoriesRepository.AddCategory(category);

                return CreatedAtAction(nameof(GetCategory), new { categoryId = categoryToCreate.CategoryId }, categoryToCreate);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating a category");
            }
        }

        // PUT: api/Categories/5
        [HttpPut("{categoryId}")]
        public async Task<ActionResult<Categories>> UpdateCategory(string categoryId, [FromBody] Categories category)
        {
            try
            {
                if (categoryId != category.CategoryId)
                {
                    return BadRequest($"Category ID mismatch");
                }

                var categoryToUpdate = await categoriesRepository.GetCategory(categoryId);

                if (categoryToUpdate == null)
                {
                    return NotFound($"Category with ID = {categoryId} cannot be found");
                }

                return await categoriesRepository.UpdateCategory(category);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating category");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<Categories>> DeleteCategory(string categoryId)
        {
            try
            {
                var categoryToDelete = await categoriesRepository.GetCategory(categoryId);

                if (categoryToDelete == null)
                {
                    return NotFound($"Category with ID = {categoryId} cannot be found");
                }

                return await categoriesRepository.DeleteCategory(categoryId);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting category");
            }
        }
    }
}
