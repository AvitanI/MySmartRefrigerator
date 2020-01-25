namespace MySmartRefrigerator.Models
{
    public interface IProductsDatabaseSettings
    {
        string ProductsCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
