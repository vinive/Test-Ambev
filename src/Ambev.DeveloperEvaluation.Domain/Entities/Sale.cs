using Ambev.DeveloperEvaluation.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class Sale
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleNumber { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string Customer { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public List<SaleItem> Items { get; set; } = new();
    public bool IsCancelled { get; set; }

    public decimal Total => Items.Sum(i => i.Total);

    public Sale(){}

    public Sale(string customer, string branch)
    {
        Customer = customer;
        Branch = branch;
    }
    public void AddItem(SaleItem item)
    {
        var existingItem = Items.FirstOrDefault(i => i.Product == item.Product);

        if (existingItem != null)
        {
            if (existingItem.UnitPrice != item.UnitPrice)
                throw new ArgumentException("Os produtos devem ter o mesmo preço.");

            if (existingItem.UnitPrice <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.");

            var newQuantity = existingItem.Quantity + item.Quantity;

            if (newQuantity > 20)
                throw new ArgumentException("Quantidade máxima de 20 unidades por produto foi atingido.");

            if (newQuantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            existingItem.Quantity = newQuantity;

        }
        else
        {
            if (item.UnitPrice <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.");

            if (item.Quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            item.Sale = this; // Aqui associa o item à venda corretamente
            Items.Add(item);
        }
    }


    public void CancelSale()
    {
        IsCancelled = true;
    }


}