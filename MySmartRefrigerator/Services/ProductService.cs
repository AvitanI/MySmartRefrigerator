using Common.DTO;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Enumerations;

namespace WebAPI.Repositories.Services
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

        /// <summary>
        /// Product prices repository for CRUD
        /// </summary>
        private readonly IProductPricesRepository _productPricesRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Init product service
        /// </summary>
        /// <param name="productRepository">Product repository instance</param>
        /// <param name="productPricesRepository">Product prices repository instance</param>
        public ProductService(IProductRepository productRepository, IProductPricesRepository productPricesRepository)
        {
            _productRepository = productRepository;
            _productPricesRepository = productPricesRepository;
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
        /// Get last updated product prices by code (barcode)
        /// </summary>
        /// <param name="code">The product code</param>
        /// <exception cref="ArgumentException">Throws when product code is empty</exception>
        /// <returns>Product prices</returns>
        public async Task<IEnumerable<UpdatedProductPrice>> GetLastUpdatedProductPricesByCodeAsync(string code)
        {
            #region Validation

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Required product code", nameof(code));
            }

            #endregion

            return await _productPricesRepository.GetLastUpdatedProductPricesByCodeAsync(code);
        }

        /// <summary>
        /// Update/Insert product
        /// </summary>
        /// <param name="productsList">The product to upsert</param>
        /// <returns>Update result of upsert operation</returns>
        /// <exception cref="ArgumentNullException">Throws when product is null</exception>
        /// <exception cref="ArgumentException">Throws when invalid chain</exception>
        /// <exception cref="ArgumentException">Throws when store id is empty</exception>
        /// <exception cref="ArgumentException">Throws when products are empty</exception>
        public async Task UpdateProductsAsync(ProductsListDTO productsList)
        {
            #region Validations

            if (productsList is null)
            {
                throw new ArgumentNullException(nameof(productsList));
            }

            if (productsList.ChainID == EChain.None)
            {
                throw new ArgumentException("Invalid chain", nameof(productsList.ChainID));
            }

            if (string.IsNullOrWhiteSpace(productsList.StoreID))
            {
                throw new ArgumentException("Store id is empty", nameof(productsList.StoreID));
            }

            if (productsList.Products.IsNullOrEmpty())
            {
                throw new ArgumentException("Products are empty", nameof(productsList.Products));
            }

            #endregion

            await UpsertProductsAsync(productsList.Products.Select(product => new Product 
            {
                Code = product.ItemCode,
                Name = product.ItemName
            }));

            await InsertProductsPricesAsync(productsList.Products.Select(product => new ProductPrice 
            { 
                Code = product.ItemCode,
                ChainID = productsList.ChainID,
                StoreID = productsList.StoreID,
                Price = Convert.ToDecimal(product.ItemPrice),
                PriceUpdateDate = product.PriceUpdateDate
            }));
        }

        private async Task InsertProductsPricesAsync(IEnumerable<ProductPrice> productsPrices)
        {
            await _productPricesRepository.InsertProductsPricesProductAsync(productsPrices);
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
