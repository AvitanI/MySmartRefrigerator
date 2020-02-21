﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.API;
using Common.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/productPrices")]
    [ApiController]
    public class ProductPricesController : ControllerBase
    {
        #region Instance Variables

        private readonly ILogger<ProductPricesController> _logger;
        private readonly IProductService _productService;

        #endregion

        #region Constructor

        public ProductPricesController(ILogger<ProductPricesController> logger, IProductService productService)
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
        [Route("getProductPricesByCode/{code}")]
        public async Task<ApiResult<IEnumerable<ProductPrice>>> GetProductPricesByCodeAsync(string code)
        {
            // Init result
            var apiResult = new ApiResult<IEnumerable<ProductPrice>>();

            try
            {
                // Get product prices from service
                apiResult.Data = await _productService.GetProductPricesByCodeAsync(code);
            }
            catch (Exception exception)
            {
                #region Write To Log (Severity: Error)

                _logger.LogError(ELogEvents.FailedToGetProductPricesByCode.ToEventID(),
                                exception,
                                "Failed to get product prices by code");

                #endregion

                apiResult.SetError();
            }

            return apiResult;
        }

        #endregion
    }
}