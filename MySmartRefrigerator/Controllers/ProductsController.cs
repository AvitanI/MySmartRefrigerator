﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WebAPI.Models;
using Common.Logs;
using Common.DTO;
using Common.API;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Represent rest for product
    /// </summary>
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        #region Instance Variables

        private readonly ILogger<ProductsController> _logger;
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
        public async Task<ApiResult<Product>> GetProductByCodeAsync(string code)
        {
            // Init result
            var apiResult = new ApiResult<Product>();
            
            try
            {
                // Get product from service
                apiResult.Data = await _productService.GetProductByCodeAsync(code);
            }
            catch (Exception exception)
            {
                #region Write To Log (Severity: Error)

                _logger.LogError(ELogEvents.FailedToGetProductByCode.ToEventID(), 
                                exception, 
                                "Failed to get product by code");

                #endregion

                apiResult.SetError();
            }

            return apiResult;
        }

        /// <summary>
        /// Update products
        /// </summary>
        /// <param name="productsList">The product to update</param>
        [HttpPost]
        [Route("updateProducts")]
        public async Task UpdateProductsAsync([FromBody] ProductsListDTO productsList)
        {
            try
            {
                await _productService.UpdateProductsAsync(productsList);
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
