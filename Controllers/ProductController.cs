using System;
using Microsoft.AspNetCore.Mvc;
using Net_Mongo.Services;
using Net_Mongo.Models;
using MongoDB.Driver;

namespace Net_Mongo.Controllers
{
    [Controller]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly MongoDBService _mongoDBService;
        public ProductController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<Product>> Get() {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product) {
            await _mongoDBService.CreateAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> Post([FromBody] List<Product> products)
        {
            await _mongoDBService.CreateBulkProducts(products);
            return CreatedAtAction(nameof(Get), products);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            await _mongoDBService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}

