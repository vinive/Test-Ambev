public interface ISaleService
{
    Task<Sale> CreateSaleAsync(Sale sale);
    Task<IEnumerable<Sale>> GetAllSalesAsync();
    Task<Sale?> GetSaleByIdAsync(Guid id);
    Task<Sale> UpdateSaleAsync(Guid id, Sale sale);
    Task CancelSaleAsync(Guid id);
}