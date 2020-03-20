namespace Common.Logs
{
    /// <summary>
    /// Represent event id for <see cref="Microsoft.Extensions.Logging.EventId"/>
    /// </summary>
    public enum ELogEvents
    {
        None = 0,
        FailedToGetProductByCode = 1,
        FailedToUpdateProducts = 2,
        FailedToGetProductPricesByCode = 3,
        FailedToGetStores = 4
    }
}
