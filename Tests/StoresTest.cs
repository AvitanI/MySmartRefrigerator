using Common.DTO.Location;
using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services;
using WebAPI.Services.Interfaces;

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
            ILocationService locationService = new LocationService();

            #endregion

            #region Act

            // Validate stores exists
            if (allStores.IsNullOrEmpty())
            {
                throw new Exception("Stores are empty");
            }

            foreach (Store store in allStores)
            {
                // Continue when store already has location
                if (store.Location != null)
                {
                    continue;
                }

                // Continue when city or address are empty
                if (string.IsNullOrWhiteSpace(store.City) ||
                    string.IsNullOrWhiteSpace(store.Address))
                {
                    continue;
                }

                // Fetch location for store
                ForwardGeocodingResponse locationResponse = 
                    await locationService.GetLocationByAddress(store.City, store.Address);

                // Continue when results are empty
                if (locationResponse.Results.IsNullOrEmpty())
                {
                    continue;
                }

                ForwardGeocodingResult result = locationResponse.Results.First();

                var geoPointToUpdate = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                                            new GeoJson2DGeographicCoordinates(
                                                longitude: result.Geometry.Lng, 
                                                latitude: result.Geometry.Lat));

                await _storeRepository.UpdateStoreLocation(store.ID, geoPointToUpdate);
            }

            #endregion

            // No assert
        }

        #endregion
    }
}
