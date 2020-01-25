using MySmartRefrigerator.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySmartRefrigerator.Services
{
    /// <summary>
    /// Represent services for product
    /// </summary>
    public class ProductService
    {
        #region Instance Variables

        /// <summary>
        /// Product repository for CRUD
        /// </summary>
        private readonly ProductRepository _productRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Init product service
        /// </summary>
        /// <param name="productRepository">Product repository instance</param>
        public ProductService(ProductRepository productRepository)
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
        /// <param name="product">The product to upsert</param>
        /// <returns>Update result of upsert operation</returns>
        /// <exception cref="ArgumentNullException">Throws when product is null</exception>
        public async Task UpsertProductAsync(IEnumerable<ProductUpdate> products)
        {
            #region Validation

            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            #endregion

            #region For Each Product - Insert Or Update

            foreach (ProductUpdate product in products)
            {
                var productToUpdate = new Product
                {
                    Code = product.ItemCode,
                    Name = product.ItemName
                };

                await _productRepository.UpsertProductAsync(productToUpdate);
            }

            #endregion
        }

        #endregion
    }
}
