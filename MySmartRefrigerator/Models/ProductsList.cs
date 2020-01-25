using System.Collections.Generic;

namespace MySmartRefrigerator.Models
{
    public class ProductsList
    {
        public IEnumerable<ProductUpdate> Products { get; set; }
    }
}
