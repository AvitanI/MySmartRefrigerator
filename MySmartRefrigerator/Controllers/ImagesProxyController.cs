using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Represent proxy for image downloading
    /// </summary>
    [Route("api/ImagesProxy")]
    [ApiController]
    public class ImagesProxyController : ControllerBase
    {
        /// <summary>
        /// Get image of product by URL
        /// </summary>
        /// <param name="url">The URL for image downloading</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getProductImage")]
        public async Task<IActionResult> GetProductImage(string url)
        {
            // Client for http calls
            using HttpClient client = new HttpClient();

            // Get the image from supplied URL
            HttpResponseMessage response = await client.GetAsync(url);

            // Throw exception if not OK status
            response.EnsureSuccessStatusCode();

            byte[] content = await response.Content.ReadAsByteArrayAsync();
            // return "data:image/png;base64," + Convert.ToBase64String(content);

            return File(content, "image/png", Guid.NewGuid().ToString());
        }
    }
}