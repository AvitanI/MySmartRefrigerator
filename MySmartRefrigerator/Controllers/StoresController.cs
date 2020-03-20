using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.API;
using Common.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        #region Instance Variables

        private readonly ILogger<StoresController> _logger;
        private readonly IStoreService _storeService;

        #endregion

        #region Constructor

        public StoresController(ILogger<StoresController> logger, IStoreService storeService)
        {
            _logger = logger;
            _storeService = storeService;
        }

        #endregion

        #region Instance Methods (API)

        /// <summary>
        /// Get all stores
        /// </summary>
        /// <returns>List of stores</returns>
        [HttpGet]
        [Route("getStores")]
        public async Task<ApiResult<IEnumerable<Store>>> GetStoresAsync()
        {
            // Init result
            var apiResult = new ApiResult<IEnumerable<Store>>();

            try
            {
                // Get stores from service
                apiResult.Data = await _storeService.GetStoresAsync();
            }
            catch (Exception exception)
            {
                #region Write To Log (Severity: Error)

                _logger.LogError(ELogEvents.FailedToGetStores.ToEventID(),
                                exception,
                                "Failed to get stores");

                #endregion

                apiResult.SetError();
            }

            return apiResult;
        }

        #endregion
    }
}