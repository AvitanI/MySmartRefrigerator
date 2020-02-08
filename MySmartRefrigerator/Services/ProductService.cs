using MySmartRefrigerator.Models;
using MySmartRefrigerator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySmartRefrigerator.Repositories
{
    /// <summary>
    /// Represent services for product
    /// </summary>
    public class ProductService : IProductService
    {
        #region Instance Variables

        /// <summary>
        /// Product repository for CRUD
        /// </summary>
        private readonly IProductRepository _productRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Init product service
        /// </summary>
        /// <param name="productRepository">Product repository instance</param>
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Get product by code (barcode)
        /// </summary>
        /// <param name="code">The product code</param>
        /// <exception cref="ArgumentException">Throws when product code is empty</exception>
        /// <returns>Product</returns>
        public async Task<Product> GetProductByCodeAsync(string code)
        {
            #region Validation

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Required product code", nameof(code));
            }

            #endregion

            return await _productRepository.GetProductByCodeAsync(code);
        }

        /// <summary>
        /// Update/Insert product
        /// </summary>
        /// <param name="products">The product to upsert</param>
        /// <returns>Update result of upsert operation</returns>
        /// <exception cref="ArgumentNullException">Throws when product is null</exception>
        public async Task UpdateProductsAsync(IEnumerable<ProductUpdate> products)
        {
            #region Validation

            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            #endregion

            await UpsertProductsAsync(products.Select(product => new Product 
            {
                Code = product.ItemCode,
                Name = product.ItemName
            }));
        }

        private async Task UpsertProductsAsync(IEnumerable<Product> products)
        {
            #region For Each Product - Insert Or Update

            foreach (Product productToUpdate in products)
            {
                await _productRepository.UpsertProductAsync(productToUpdate);
            }

            #endregion
        }

        #endregion
    }
}
