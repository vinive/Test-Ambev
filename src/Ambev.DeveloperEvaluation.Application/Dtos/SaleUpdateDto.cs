public class SaleUpdateDto
{
    public string? Customer { get; set; }
    public string? Branch { get; set; }
    public List<SaleItem>? Items { get; set; }
}