using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MySmartRefrigerator.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }


        public DateTime LastUpdate { get; set; }
    }
}
