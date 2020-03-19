using Common.Enumerations;
using System.Collections.Generic;

namespace Common.DTO
{
    public class ProductsListDTO
    {
        /// <summary>
        /// The internal chain id
        /// </summary>
        public EChain ChainID { get; set; }

        /// <summary>
        /// The store id as it saved in provider
        /// </summary>
        public string StoreID { get; set; }

        public IEnumerable<ProductUpdateDTO> Products { get; set; }
    }
}
