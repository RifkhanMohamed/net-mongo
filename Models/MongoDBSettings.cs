using System;
namespace Net_Mongo.Models
{
    public class MongoDBSettings
    {
        public MongoDBSettings()
        {
        }

        public string ConnectionURI { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string ProductCollectionName { get; set; } = null;
        public string CategoryCollectionNAme { get; set; } = null;
    }
}

