using System.Collections.Generic;

namespace Common.DTO
{
    public class ProductsListDTO
    {
        public IEnumerable<ProductUpdateDTO> Products { get; set; }
    }
}
