using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    /// <summary>
    /// Represent services for store
    /// </summary>
    public class StoreService : IStoreService
    {
        #region Instance Variables

        /// <summary>
        /// Product repository for CRUD
        /// </summary>
        private readonly IStoreRepository _storeRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Init store service
        /// </summary>
        /// <param name="storeRepository">Store repository instance</param>
        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Get all stores
        /// </summary>
        /// <returns>List of stores</returns>
        public async Task<IEnumerable<Store>> GetStoresAsync()
        {
            return await _storeRepository.GetStoresAsync();
        }

        #endregion
    }
}
