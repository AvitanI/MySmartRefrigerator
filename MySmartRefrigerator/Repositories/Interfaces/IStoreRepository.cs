using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Repositories.Interfaces
{
    /// <summary>
    /// Represent CRUD operation for Stores collection
    /// </summary>
    public interface IStoreRepository
    {
        /// <summary>
        /// Get all stores
        /// </summary>
        /// <returns>List of stores</returns>
        Task<IEnumerable<Store>> GetStoresAsync();

        /// <summary>
        /// Updates store location
        /// </summary>
        /// <param name="id">The document id of store to update</param>
        /// <param name="location">The location to update</param>
        /// <returns>Result of update</returns>
        Task<UpdateResult> UpdateStoreLocation(string id,
                                                GeoJsonPoint<GeoJson2DGeographicCoordinates> location);
    }
}
