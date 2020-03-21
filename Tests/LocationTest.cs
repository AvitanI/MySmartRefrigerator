using Common.DTO.Location;
using Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WebAPI.Services;
using WebAPI.Services.Interfaces;

namespace Tests
{
    /// <summary>
    /// Represent tests for locations
    /// </summary>
    [TestClass]
    public class LocationTest
    {
        #region Instance Methods

        /// <summary>
        /// Validate that returned locations for address
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WebAPI_LocationService_GetLocationByAddress_Should_Fetch_Locations_By_Address()
        {
            #region Arrange

            ILocationService service = new LocationService();

            string city = "קרית ביאליק";
            string address = "דרך עכו";

            #endregion

            #region Act

            ForwardGeocodingResponse response = await service.GetLocationByAddress(city, address);

            #endregion

            #region Assert

            Assert.IsTrue(  response != null                    &&
                            !response.Results.IsNullOrEmpty()   &&
                            response.Results.Count > 0);

            #endregion
        }

        #endregion
    }
}
