using MongoDB.Driver;
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

        #endregion
    }
}
