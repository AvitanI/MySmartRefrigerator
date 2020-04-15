using Common.DTO;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Enumerations;
using System.Text.RegularExpressions;

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

        #region Class Variables

        private static readonly string group_name_for_pattern = "unit";

        /// <summary>
        /// Used for regex
        /// </summary>
        private static readonly string UNIT_OF_MEASURE_PATTERN = 
            $@"((?<{group_name_for_pattern}>\d+)\s+|[""a-z\u0590-\u05fe]+)";

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

            #region Upsert Products

            var productModels = new List<Product>(productsList.Products.Count());

            foreach (var product in productsList.Products)
            {
                decimal.TryParse(product.Quantity, out decimal quantity);
                decimal.TryParse(product.UnitOfMeasurePrice, out decimal unitOfMeasurePrice);

                await _productRepository.UpsertProductAsync(new Product
                {
                    Code = product.ItemCode,
                    Name = product.ItemName,
                    ManufacturerName = product.ManufacturerName,
                    ManufactureCountry = product.ManufactureCountry,
                    ManufacturerDescription = product.ManufacturerItemDescription,
                    Quantity = quantity,
                    UnitQuantityType = product.UnitQty.ToUnitQuantity(),
                    UnitOfMeasure = ExtractUnitOfMeasure(product.UnitOfMeasure),
                    UnitOfMeasurePrice = unitOfMeasurePrice
                });
            }

            #endregion

            #region Insert Product Prices

            await _productPricesRepository.InsertProductsPricesProductAsync(productsList.Products.Select(product => 
                new ProductPrice 
                { 
                    Code = product.ItemCode,
                    ChainID = productsList.ChainID,
                    StoreID = Convert.ToInt32(productsList.StoreID),
                    Price = Convert.ToDecimal(product.ItemPrice),
                    PriceUpdateDate = product.PriceUpdateDate
                }));

            #endregion
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Get unit of measure by string e.g. 100 מ"ל
        /// </summary>
        /// <param name="unitOfMeasure">The unit of measure</param>
        /// <returns></returns>
        private static decimal ExtractUnitOfMeasure(string unitOfMeasure)
        {
            // Check for empty value
            if (string.IsNullOrWhiteSpace(unitOfMeasure))
            {
                return default;
            }

            var regex = new Regex(UNIT_OF_MEASURE_PATTERN);

            // Mtaching unit e.g. 100 מ"ל or ק"ג
            Match match = regex.Match(unitOfMeasure);

            if (match.Success)
            {
                string unit = match.Groups[group_name_for_pattern].Value;

                decimal.TryParse(unit, out decimal parsedUnitOfMeasure);

                return parsedUnitOfMeasure;
            }
            else
            {
                return default;
            }
        }

        #endregion
    }
}
