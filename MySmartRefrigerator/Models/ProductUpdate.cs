using System;

namespace MySmartRefrigerator.Models
{
    public class ProductUpdate
    {
        public EChain ChainID { get; set; }

        public DateTime PriceUpdateDate { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string ItemPrice { get; set; }
    }
}
