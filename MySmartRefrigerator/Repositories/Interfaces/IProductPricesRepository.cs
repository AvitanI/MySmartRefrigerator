using MySmartRefrigerator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySmartRefrigerator.Repositories.Interfaces
{
    /// <summary>
    /// Represent CRUD operation for ProductPrices collection
    /// </summary>
    public interface IProductPricesRepository
    {
        /// <summary>
        /// Insert products prices
        /// </summary>
        /// <param name="productsPrices">The products prices to insert</param>
        Task InsertProductsPricesProductAsync(IEnumerable<ProductPrice> productsPrices);
    }
}
