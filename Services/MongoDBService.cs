using System;
using Net_Mongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Net_Mongo.Services
{
    public class MongoDBService
    {

        private readonly IMongoCollection<Product> _productCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _productCollection = database.GetCollection<Product>(mongoDBSettings.Value.ProductCollectionName);
        }

        public async Task<List<Product>> GetProductsByIdsAsync(List<string> productIds)
        {
            var objectIdList = productIds.Select(id => ObjectId.Parse(id)).ToList();
            var filter = Builders<Product>.Filter.In("_id", objectIdList);
            return await _productCollection.Find(filter).ToListAsync();
        }

        public async Task CreateAsync(Product product)
        {
            await _productCollection.InsertOneAsync(product);
            return;
        }

        public async Task<List<Product>> CreateBulkProducts(List<Product> products)
        {  
            await _productCollection.InsertManyAsync(products);
            return products;
        }

        public async Task<List<Product>> GetAsync()
        {
            return await _productCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(x=>x.Id,product.Id);
            await _productCollection.ReplaceOneAsync(filter,product);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("Id", id);
            await _productCollection.DeleteOneAsync(filter);
            return;
        }
    }
}

