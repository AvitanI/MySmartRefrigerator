using Common.DTO.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services.Interfaces
{
    /// <summary>
    /// Represent set of actions for locations
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Fetch forward geocoding result from Open Cage API
        /// </summary>
        /// <param name="city">The city for location</param>
        /// <param name="address">The address for location</param>
        /// <returns></returns>
        Task<ForwardGeocodingResponse> GetLocationByAddress(string city, string address);
    }
}
