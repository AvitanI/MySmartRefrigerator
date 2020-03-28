using System;

namespace Common.DTO
{
    /*
     * Example of input:
     * 
     * <PriceUpdateDate>2017-02-07 08:54</PriceUpdateDate>
      <ItemCode>11210000094</ItemCode>
      <ItemType>1</ItemType>
      <ItemName>רוטב טבסקו 60 מ"ל</ItemName>
      <ManufacturerName>ניצן</ManufacturerName>
      <ManufactureCountry>US</ManufactureCountry>
      <ManufacturerItemDescription>רוטב טבסקו</ManufacturerItemDescription>
      <UnitQty>מיליליטרים</UnitQty>
      <Quantity>60.00</Quantity>
      <bIsWeighted>0</bIsWeighted>
      <UnitOfMeasure>100 מ"ל</UnitOfMeasure>
      <QtyInPackage>0</QtyInPackage>
      <ItemPrice>12.80</ItemPrice>
      <UnitOfMeasurePrice>21.33</UnitOfMeasurePrice>
      <AllowDiscount>1</AllowDiscount>
      <ItemStatus>1</ItemStatus>
     */

    public class ProductUpdateDTO
    {
        public DateTime PriceUpdateDate { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string ItemPrice { get; set; }

        public string ManufacturerName { get; set; }

        public string ManufactureCountry { get; set; }

        public string ManufacturerItemDescription { get; set; }

        public string Quantity { get; set; }

        public string UnitQty { get; set; }

        public string UnitOfMeasure { get; set; }

        public string UnitOfMeasurePrice { get; set; }
    }
}
