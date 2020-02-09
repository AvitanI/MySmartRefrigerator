using MongoDB.Driver;
using MySmartRefrigerator.Models;
using MySmartRefrigerator.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MySmartRefrigerator.Repositories
{
    /// <summary>
    /// Represent CRUD operation for ProductsPrices collection
    /// </summary>
    public class ProductPricesRepository : IProductPricesRepository
    {
        #region Class Variables

        private static readonly int CommandTimeoutInMS = 30000;

        #endregion

        #region Instance Variables

        /// <summary>
        /// Represents products collection in MongoDB
        /// </summary>
        private readonly IMongoCollection<ProductPrice> _productsPrices;

        #endregion

        #region Constructor

        /// <summary>
        /// Init product prices repository instance
        /// </summary>
        /// <param name="settings"></param>
        public ProductPricesRepository(IProductsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

            _productsPrices = database.GetCollection<ProductPrice>(settings.ProductsPricesCollectionName);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Update/Insert product
        /// </summary>
        /// <param name="product">The product to upsert</param>
        /// <returns>Update result of upsert operation</returns>
        /// <exception cref="ArgumentNullException">Throws when product is null</exception>
        public async Task InsertProductsPricesProductAsync(IEnumerable<ProductPrice> productsPrices)
        {
            // Set cancellation token
            var cancellationTokenSource = new CancellationTokenSource(CommandTimeoutInMS);

            await _productsPrices.InsertManyAsync(productsPrices, cancellationToken: cancellationTokenSource.Token);
        }

        #endregion
    }
}
