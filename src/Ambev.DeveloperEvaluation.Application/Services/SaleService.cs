public class SaleService : ISaleService
{
    private readonly ISaleRepository _repository;

    public SaleService(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Sale> CreateSaleAsync(Sale sale)
    {
        var grouped = sale.Items
            .GroupBy(i => i.Product)
            .Select(g =>
            {
                var unitPrices = g.Select(i => i.UnitPrice).Distinct().ToList();

                if (unitPrices.Count > 1)
                    throw new ArgumentException($"Produto '{g.Key}' com preços diferentes.");

                return new SaleItem
                {
                    Product = g.Key,
                    Quantity = g.Sum(i => i.Quantity),
                    UnitPrice = unitPrices.First()
                };
            }).ToList();

        sale.Items = new List<SaleItem>();

        foreach (var item in grouped)
        {
            sale.AddItem(item);
        }

        sale.SaleNumber = await _repository.GetLastSaleNumberAsync() + 1;

        await _repository.AddAsync(sale);
        return sale;
    }

    public async Task<Sale?> GetSaleByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Sale>> GetAllSalesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Sale> UpdateSaleAsync(Guid saleId, Sale updatedSale)
    {
        var existingSale = await _repository.GetByIdAsync(saleId);
        if (existingSale == null)
        {
            throw new ArgumentException("Venda não encontrada");
        }

        existingSale.Customer = updatedSale.Customer;
        existingSale.Branch = updatedSale.Branch;

        var updatedItemIds = updatedSale.Items.Select(i => i.Id).ToList();

        // 1. Remover itens que não estão mais na venda atualizada
        var itemsToRemove = existingSale.Items
            .Where(i => !updatedItemIds.Contains(i.Id))
            .ToList();

        foreach (var item in itemsToRemove)
        {
            existingSale.Items.Remove(item);
        }


        foreach (var item in updatedSale.Items)
        {
            var existingItem = existingSale.Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {

                existingItem.Quantity = item.Quantity;
                existingItem.UnitPrice = item.UnitPrice;
                existingItem.SaleId = saleId;
            }
            else
            {

                item.SaleId = saleId;
                existingSale.AddItem(item);
            }
        }


        await _repository.UpdateAsync(existingSale);
        return existingSale;
    }
    public async Task CancelSaleAsync(Guid id)
    {
        var sale = await _repository.GetByIdAsync(id);

        if (sale is null)
            throw new ArgumentException("Venda não encontrada");

        sale.CancelSale();
        await _repository.UpdateAsync(sale);
    }
}
