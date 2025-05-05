public class SaleUpdateDto
{
    public string Customer { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public List<SaleItemUpdateDto> Items { get; set; } = new();
}