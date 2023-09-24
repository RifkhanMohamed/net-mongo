using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace Net_Mongo.Models
{
    public class Category
    {
        public Category()
        {

        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string categoryName { get; set; }

        public List<string> productIds { get; set; } = null!;
    }
}

