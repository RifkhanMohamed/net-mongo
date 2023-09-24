using System;
using Net_Mongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Net_Mongo.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(mongoDBSettings.Value.CategoryCollectionNAme);
        }

        public List<string> GetProductIdsByCategoryName(string categoryName)
        {
            var filter = Builders<Category>.Filter.Eq("categoryName", categoryName);
            var projection = Builders<Category>.Projection.Include("productIds");

            var category = _categoryCollection.Find(filter)
                .Project(projection)
                .FirstOrDefault();

            if (category != null && category.Contains("productIds"))
            {
                var productIds = category["productIds"].AsBsonArray;
                return productIds.Select(id => id.ToString()).ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        public async Task CreateAsync(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);
            return;
        }

        public async Task<List<Category>> GetAsync()
        {
            return await _categoryCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddToCategoryAsync(string id, string productId)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq("Id", id);
            UpdateDefinition<Category> update = Builders<Category>.Update.AddToSet<string>("productIds", productId);
            await _categoryCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq("Id", id);
            await _categoryCollection.DeleteOneAsync(filter);
            return;
        }
    }
}

