using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Tests
{
    /// <summary>
    /// Represent tests for products
    /// </summary>
    [TestClass]
    public class ProductsTest
    {
        #region Instance Variables

        private readonly IProductsDatabaseSettings _settings;
        private readonly IProductRepository _productRepository;
        private readonly IProductPricesRepository _productPricesRepository;

        #endregion

        #region Constructor

        public ProductsTest()
        {
            _settings = new ProductsDatabaseSettings 
            {
                ProductsCollectionName = "Products",
                ProductsPricesCollectionName = "ProductsPrices",
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "SupermarketProducts"
            };

            _productRepository = new ProductRepository(_settings);
            _productPricesRepository = new ProductPricesRepository(_settings);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Validate empty product code case
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "An empty product code inappropriately allowed.")]
        public async Task MySmartRefrigerator_ProductService_GetProductByCodeAsync_Should_Throw_Argument_Exception()
        {
            #region Arrange

            var emptyCode = string.Empty;

            IProductService productService = new ProductService(_productRepository, _productPricesRepository);

            #endregion

            #region Act

            await productService.GetProductByCodeAsync(code: emptyCode);

            #endregion

            // No need to add assert region since
            // Exception should be already thrown.
        }
        
        #endregion
    }
}
