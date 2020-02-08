using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using MySmartRefrigerator.Repositories;
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
        private readonly IProductService _productService;

        #endregion

        #region Constructor

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
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
                product = await _productService.GetProductByCodeAsync(code);
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
                await _productService.UpdateProductsAsync(products.Products);
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
