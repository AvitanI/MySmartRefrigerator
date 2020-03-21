using Common.DTO.Location;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    /// <summary>
    /// Represent set of actions for locations
    /// </summary>
    public class LocationService : ILocationService
    {
        #region Instance Methods

        /// <summary>
        /// Fetch forward geocoding result from Open Cage API
        /// </summary>
        /// <param name="city">The city for location</param>
        /// <param name="address">The address for location</param>
        /// <returns></returns>
        public async Task<ForwardGeocodingResponse> GetLocationByAddress(string city, string address)
        {
            #region Validations

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City is empty", nameof(city));
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Address is empty", nameof(address));
            }

            #endregion

            using var client = new HttpClient();

            // Get http response
            HttpResponseMessage response = await client.GetAsync($"https://api.opencagedata.com/geocode/v1/json?key=e00732608e964316b6532ac36520dd4a&q={address} ,{city}&pretty=1&countrycode=IL&language=he");
            
            // Gets response in string
            string content = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<ForwardGeocodingResponse>(content);
        }

        #endregion
    }
}
