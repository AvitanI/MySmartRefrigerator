using Common.DTO;
using WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Repositories.Interfaces
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
        /// <param name="productsList">The product to upsert</param>
        /// <returns>Task</returns>
        Task UpdateProductsAsync(ProductsListDTO productsList);

        /// <summary>
        /// Get product prices by code (barcode)
        /// </summary>
        /// <param name="code">The product code</param>
        /// <returns>Product prices</returns>
        public Task<IEnumerable<ProductPrice>> GetProductPricesByCodeAsync(string code);
    }
}
