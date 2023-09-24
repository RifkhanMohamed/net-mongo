using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace Net_Mongo.Models
{
    public class Product
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? title { get; set; }

        public string? price { get; set; }

        public string? product_details { get; set; }

        public string? image_url { get; set; }

        public int quantity { get; set; }
    }
}

