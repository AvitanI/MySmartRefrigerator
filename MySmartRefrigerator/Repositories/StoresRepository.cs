using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class StoresRepository : IStoreRepository
    {
        #region Class Variables

        private static readonly int CommandTimeoutInMS = 30000;

        #endregion

        #region Instance Variables

        /// <summary>
        /// Represents products collection in MongoDB
        /// </summary>
        private readonly IMongoCollection<Store> _stores;

        #endregion

        #region Constructor

        /// <summary>
        /// Init store repository instance
        /// </summary>
        /// <param name="settings"></param>
        public StoresRepository(IProductsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

            _stores = database.GetCollection<Store>(settings.StoresCollectionName);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Get all stores
        /// </summary>
        /// <returns>List of stores</returns>
        public async Task<IEnumerable<Store>> GetStoresAsync()
        {
            // Set cancellation token
            var cancellationTokenSource = new CancellationTokenSource(CommandTimeoutInMS);

            return await _stores.Find(Builders<Store>.Filter.Empty)
                                .ToListAsync(cancellationToken: cancellationTokenSource.Token);
        }

        /// <summary>
        /// Updates store location
        /// </summary>
        /// <param name="id">The document id of store to update</param>
        /// <param name="location">The location to update</param>
        /// <exception cref="ArgumentException">Throws when id is empty</exception>
        /// <exception cref="ArgumentNullException">Throws when location is null</exception>
        /// <returns>Result of update</returns>
        public async Task<UpdateResult> UpdateStoreLocation(string id, 
                                                            GeoJsonPoint<GeoJson2DGeographicCoordinates> location)
        {
            #region Validations

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("MongoDB id is empty", nameof(id));
            }

            if (location is null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            #endregion

            // Create filter
            FilterDefinition<Store> filter = Builders<Store>.Filter.Eq(store => store.ID, id);

            // Create update query
            UpdateDefinition<Store> update = Builders<Store>.Update.Set(store => store.Location, location);

            return await _stores.UpdateOneAsync(filter, update);
        }

        #endregion
    }
}
