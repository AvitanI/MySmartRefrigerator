using MongoDB.Driver;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Repositories
{
    /// <summary>
    /// Represent CRUD operation for Products collection
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        #region Class Variables

        private static readonly int CommandTimeoutInMS = 30000;

        #endregion

        #region Instance Variables

        /// <summary>
        /// Represents products collection in MongoDB
        /// </summary>
        private readonly IMongoCollection<Product> _products;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Init product repository instance
        /// </summary>
        /// <param name="settings"></param>
        public ProductRepository(IProductsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Product>(settings.ProductsCollectionName);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Get product by code (barcode)
        /// </summary>
        /// <param name="code">The product code</param>
        /// <exception cref="ArgumentException">Throws when product code is empty</exception>
        /// <returns>Product</returns>
        public async Task<Product> GetProductByCodeAsync(string code)
        {
            #region Validation

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Required product code", nameof(code));
            }

            #endregion

            // Create filter
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Code, code);

            // Set cancellation token
            var cancellationTokenSource = new CancellationTokenSource(CommandTimeoutInMS);

            return await _products.Find(filter).FirstOrDefaultAsync(cancellationToken: cancellationTokenSource.Token);
        }

        /// <summary>
        /// Update/Insert product
        /// </summary>
        /// <param name="product">The product to upsert</param>
        /// <returns>Update result of upsert operation</returns>
        /// <exception cref="ArgumentNullException">Throws when product is null</exception>
        public async Task<UpdateResult> UpsertProductAsync(Product product)
        {
            #region Validation

            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            #endregion

            #region Create Filter

            FilterDefinition<Product> filter =
                    Builders<Product>.Filter.Eq(p => p.Code, product.Code);

            #endregion

            #region Create Update

            UpdateDefinition<Product> update =
                Builders<Product>.Update.Set(p => p.LastUpdate, DateTime.Now)
                                        .SetOnInsert(p => p.Code, product.Code)
                                        .SetOnInsert(p => p.Name, product.Name)
                                        .SetOnInsert(p => p.ManufacturerName, product.ManufacturerName)
                                        .SetOnInsert(p => p.ManufactureCountry, product.ManufactureCountry)
                                        .SetOnInsert(p => p.ManufacturerDescription, product.ManufacturerDescription)
                                        .SetOnInsert(p => p.Quantity, product.Quantity)
                                        .SetOnInsert(p => p.UnitQuantityType, product.UnitQuantityType)
                                        .SetOnInsert(p => p.UnitOfMeasure, product.UnitOfMeasure)
                                        .SetOnInsert(p => p.UnitOfMeasurePrice, product.UnitOfMeasurePrice);

        #endregion

        // Set cancellation token
        var cancellationTokenSource = new CancellationTokenSource(CommandTimeoutInMS);

            return await _products.UpdateOneAsync(  filter, update, 
                                                    new UpdateOptions { IsUpsert = true }, 
                                                    cancellationToken: cancellationTokenSource.Token);
        }

        #endregion
    }
}
