using Common.Enumerations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WebAPI.Models
{
    /// <summary>
    /// Represent model for product price
    /// </summary>
    public class ProductPrice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        /// <summary>
        /// The barcode of product
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The attached chain
        /// </summary>
        public EChain ChainID { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The date price updated
        /// </summary>
        public DateTime PriceUpdateDate { get; set; }
    }
}
