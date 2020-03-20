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
    }
}
