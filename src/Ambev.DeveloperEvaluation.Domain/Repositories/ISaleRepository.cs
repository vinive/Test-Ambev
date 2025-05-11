public interface ISaleRepository 
{
    Task<Sale?> GetByIdAsync(Guid id);
    Task<IEnumerable<Sale>> GetAllAsync();
    Task AddAsync(Sale sale);   
    Task UpdateAsync(Sale sale);
    Task DeleteAsync(Sale sale);  
    Task<int> GetLastSaleNumberAsync();
}