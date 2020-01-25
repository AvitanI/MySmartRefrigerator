using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using MySmartRefrigerator.Services;
using MySmartRefrigerator.Models;
using Common.Logs;

namespace MySmartRefrigerator.Controllers
{
    /// <summary>
    /// Represent rest for product
    /// </summary>
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        #region Instance Variables

        private readonly ILogger<dynamic> _logger;
        private readonly ProductRepository _productRepository;

        #endregion

        #region Constructor

        public ProductsController(ILogger<dynamic> logger, ProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        #endregion

        #region Instance Methods (API)

        /// <summary>
        /// Get product by code (barcode)
        /// </summary>
        /// <param name="code">The product code</param>
        /// <returns>Product</returns>
        [HttpGet]
        [Route("getProductByCode/{code}")]
        public async Task<Product> GetProductByCodeAsync(string code)
        {
            Product product = null;

            try
            {
                product = await new ProductService(_productRepository).GetProductByCodeAsync(code);
            }
            catch (Exception exception)
            {
                #region Write To Log (Severity: Error)

                _logger.LogError(ELogEvents.FailedToGetProductByCode.ToEventID(), 
                                exception, 
                                "Failed to get product by code");

                #endregion
            }

            return product;
        }

        /// <summary>
        /// Update products
        /// </summary>
        /// <param name="products">The product to update</param>
        [HttpPost]
        [Route("updateProducts")]
        public async Task UpdateProductsAsync([FromBody] ProductsList products)
        {
            try
            {
                await new ProductService(_productRepository).UpsertProductAsync(products.Products);
            }
            catch (Exception exception)
            {
                #region Write To Log (Severity: Error)

                _logger.LogError(ELogEvents.FailedToUpdateProducts.ToEventID(),
                                exception,
                                "Failed to update products");

                #endregion
            }
        }
        
        #endregion
    }
}
