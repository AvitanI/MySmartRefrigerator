using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    /// <summary>
    /// Represent services for store
    /// </summary>
    public interface IStoreService
    {
        /// <summary>
        /// Get all stores
        /// </summary>
        /// <returns>List of stores</returns>
        Task<IEnumerable<Store>> GetStoresAsync();
    }
}
