using Common.Enumerations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace WebAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Store
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        /// <summary>
        /// The internal chain id
        /// </summary>
        public EChain InternalChainID { get; set; }

        /// <summary>
        /// The store id of provider
        /// </summary>
        public string StoreID { get; set; }

        /// <summary>
        /// The chain name of provider
        /// </summary>
        public string ChainName { get; set; }

        /// <summary>
        /// The sub chain name of provider
        /// </summary>
        public string SubChainName { get; set; }

        /// <summary>
        /// The store name of provider
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// The address of provider
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The city of provider
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The lat and lon of store.
        /// <seealso cref="https://stackoverflow.com/questions/49779378/how-to-store-and-query-an-array-of-coordinates-in-c-sharp-mongodb-strongly-typed"/>
        /// </summary>
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
    }
}
