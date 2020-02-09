namespace WebAPI.Models
{
    public class ProductsDatabaseSettings : IProductsDatabaseSettings
    {
        public string ProductsCollectionName { get; set; }

        public string ProductsPricesCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
