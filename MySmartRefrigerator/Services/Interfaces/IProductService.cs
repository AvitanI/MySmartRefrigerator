using MySmartRefrigerator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySmartRefrigerator.Repositories
{
    /// <summary>
    /// Represent services for product
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get product by code (barcode)
        /// </summary>
        /// <param name="code">The product code</param>
        /// <returns>Product</returns>
        Task<Product> GetProductByCodeAsync(string code);

        /// <summary>
        /// Update/Insert product
        /// </summary>
        /// <param name="products">The product to upsert</param>
        /// <returns>Task</returns>
        Task UpdateProductsAsync(IEnumerable<ProductUpdate> products);
    }
}
