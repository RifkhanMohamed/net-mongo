using System;
using Microsoft.AspNetCore.Mvc;
using Net_Mongo.Services;
using Net_Mongo.Models;
using MongoDB.Driver;

namespace Net_Mongo.Controllers
{
    [Controller]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly MongoDBService _mongoDBService;
        public CategoryController(CategoryService categoryService, MongoDBService mongoDBService)
        {
            _categoryService = categoryService;
            _mongoDBService = mongoDBService;
        }

        [HttpGet("{categoryName}")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategory(string categoryName)
        {
            List<string> categoryProductIds = _categoryService.GetProductIdsByCategoryName(categoryName);

            if (categoryProductIds == null)
                return NotFound();

            var products = await _mongoDBService.GetProductsByIdsAsync(categoryProductIds);
            return Ok(products);
        }

        [HttpGet]
        public async Task<List<Category>> Get()
        {
            return await _categoryService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            await _categoryService.CreateAsync(category);
            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddToCategory(string id, [FromBody] string productId)
        {
            await _categoryService.AddToCategoryAsync(id,productId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}

