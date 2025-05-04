using Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int SaleNumber { get; set; } 
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string Customer { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public List<SaleItem> Items { get; set; } = new();
    public bool IsCancelled { get; set; }

    public decimal Total => Items.Sum(i => i.Total);

    public void CancelSale()
    {
        IsCancelled = true;
    }

    public void AddItem(SaleItem item)
    {
        if (item.Quantity > 20)
            throw new ArgumentException("Não é permitido vender mais de 20 unidade do mesmo produto.");

        Items.Add(item);
    }
}