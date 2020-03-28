using Common.Enumerations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WebAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ManufacturerName { get; set; }

        public string ManufactureCountry { get; set; }

        public string ManufacturerDescription { get; set; }

        public decimal Quantity { get; set; }

        public EUnitQuantity UnitQuantityType { get; set; }

        public decimal UnitOfMeasure { get; set; }

        public decimal UnitOfMeasurePrice { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
