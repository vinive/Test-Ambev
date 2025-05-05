using System.Text.Json.Serialization;

public class SaleItem
{
    public int Id {get; set; }
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public bool Status { get; set; } = true;
    public decimal Discount => CalculateDiscount();
    public decimal Total => (UnitPrice * Quantity) - Discount;

    public Guid SaleId { get; set; }
    
    [JsonIgnore]
    public Sale Sale { get; set; }

    private decimal CalculateDiscount()
    {
        if (Quantity >= 10 && Quantity <= 20)
            return UnitPrice * Quantity * 0.20m;

        if (Quantity >= 4)
            return UnitPrice * Quantity * 0.10m;

        return 0;
    }
}