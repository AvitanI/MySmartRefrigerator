using MongoDB.Driver;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Repositories
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
        /// Get product prices by code
        /// </summary>
        /// <param name="code">The product code</param>
        /// <exception cref="ArgumentException">Throws when product code is empty</exception>
        /// <returns>Product prices</returns>
        public async Task<IEnumerable<ProductPrice>> GetProductPricesByCodeAsync(string code)
        {
            #region Validation

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Required product code", nameof(code));
            }

            #endregion

            // Create filter
            FilterDefinition<ProductPrice> filter = Builders<ProductPrice>.Filter.Eq(p => p.Code, code);

            // Set cancellation token
            var cancellationTokenSource = new CancellationTokenSource(CommandTimeoutInMS);

            return await _productsPrices.Find(filter).ToListAsync(cancellationToken: cancellationTokenSource.Token);
        }

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
