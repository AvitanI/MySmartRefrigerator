using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;

namespace Tests
{
    /// <summary>
    /// Represent tests for stores
    /// </summary>
    [TestClass]
    public class StoresTest
    {
        #region Instance Variables

        private readonly IProductsDatabaseSettings _settings;
        private readonly IStoreRepository _storeRepository;

        #endregion

        #region Constructor

        public StoresTest()
        {
            _settings = new ProductsDatabaseSettings
            {
                StoresCollectionName = "Stores",
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "SupermarketProducts"
            };

            _storeRepository = new StoresRepository(_settings);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Update location for each store
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WebAPI_StoreRepository_UpdateStoreLocation_Should_Update_For_Each_Store()
        {
            #region Arrange

            IEnumerable<Store> allStores = await _storeRepository.GetStoresAsync();

            #endregion

            #region Act

            // Validate stores exists
            if (allStores.IsNullOrEmpty())
            {
                throw new Exception("Stores are empty");
            }

            UpdateResult updateResult = await _storeRepository.UpdateStoreLocation(allStores.First().ID,
                                                        new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                                                            new GeoJson2DGeographicCoordinates(longitude: 35.084962, latitude: 32.83619)));

            #endregion

            #region Assert

            Assert.IsTrue(updateResult.MatchedCount > 0 && updateResult.ModifiedCount > 0);

            #endregion
        }

        #endregion
    }
}
