using MongoDB.Driver;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

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
        /// Get last updated product prices by code
        /// </summary>
        /// <param name="code">The product code</param>
        /// <exception cref="ArgumentException">Throws when product code is empty</exception>
        /// <returns>Product prices</returns>
        public async Task<IEnumerable<UpdatedProductPrice>> GetLastUpdatedProductPricesByCodeAsync(string code)
        {
            #region Validation

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Required product code", nameof(code));
            }

            #endregion

            #region Query Output

            /*
             * db.getCollection('ProductsPrices').aggregate([
                    {
                        $sort: { 
                            PriceUpdateDate: -1
                        }
                    },
                    {
                        $match: {
                            Code : '16000548909'
                        }
                    },
                    {
                        $group: {
                          _id: { chainID : "$ChainID", storeID : "$StoreID" },
                          price: { $first : "$Price" }
                        }
                    },
                    {
                        $limit: 10
                    },
                    {
                        $sort: { 
                            price: 1
                        }
                    },
                    {
                        $project: {
                            _id: 0,
                            ChainID: '$_id.chainID',
                            StoreID: '$_id.storeID',
                            Price: '$price'
                        }
                    }
                ])
             */

            #endregion

            // Set cancellation token
            var cancellationTokenSource = new CancellationTokenSource(CommandTimeoutInMS);

            return await _productsPrices.Aggregate()
                                        .SortByDescending(productPrice => productPrice.PriceUpdateDate)
                                        .Match(Builders<ProductPrice>.Filter.Eq(productPrice => productPrice.Code, code))
                                        .Group(
                                            productPrice => new 
                                            {
                                                chainID = productPrice.ChainID,
                                                storeID = productPrice.StoreID 
                                            },
                                            group => new {
                                                price = group.Select(
                                                    productPrice => productPrice.Price
                                                ).First()
                                            })
                                        .SortBy(productPrice => productPrice.price)
                                        // TO-DO: Fix this magic string to static code
                                        .Project<UpdatedProductPrice>(@"
                                                {
                                                    _id: 0,
                                                    ChainID: '$_id.chainID',
                                                    StoreID: '$_id.storeID',
                                                    Price: '$price'
                                                }")
                                        .ToListAsync(cancellationToken: cancellationTokenSource.Token);
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
