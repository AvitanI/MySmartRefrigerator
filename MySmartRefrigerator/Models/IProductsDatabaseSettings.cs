namespace WebAPI.Models
{
    public interface IProductsDatabaseSettings
    {
        string ProductsCollectionName { get; set; }

        string ProductsPricesCollectionName { get; set; }

        string StoresCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
