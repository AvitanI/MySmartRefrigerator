using WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Repositories.Interfaces
{
    /// <summary>
    /// Represent CRUD operation for ProductPrices collection
    /// </summary>
    public interface IProductPricesRepository
    {
        /// <summary>
        /// Get last updated product prices by code
        /// </summary>
        /// <param name="code">The product code</param>
        /// <returns>Product prices</returns>
        public Task<IEnumerable<UpdatedProductPrice>> GetLastUpdatedProductPricesByCodeAsync(string code);

        /// <summary>
        /// Insert products prices
        /// </summary>
        /// <param name="productsPrices">The products prices to insert</param>
        Task InsertProductsPricesProductAsync(IEnumerable<ProductPrice> productsPrices);
    }
}
