using Common.Enumerations;

namespace WebAPI.Models
{
    /// <summary>
    /// Represent result of last updated product price
    /// </summary>
    public class UpdatedProductPrice
    {
        /// <summary>
        /// The attached chain
        /// </summary>
        public EChain ChainID { get; set; }

        /// <summary>
        /// The store id
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
    }
}
