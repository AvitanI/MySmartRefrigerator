using MongoDB.Driver;
using WebAPI.Models;
using System.Threading.Tasks;

namespace WebAPI.Repositories.Interfaces
{
    /// <summary>
    /// Represent CRUD operation for Products collection
    /// </summary>
    public interface IProductRepository
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
        /// <param name="product">The product to upsert</param>
        /// <returns>Update result of upsert operation</returns>
        Task<UpdateResult> UpsertProductAsync(Product product);
    }
}
