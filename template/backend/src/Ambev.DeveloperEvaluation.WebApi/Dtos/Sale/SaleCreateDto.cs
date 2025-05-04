public class SaleCreateDto
{
    public string Customer { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public List<SaleItemCreateDto> Items {get; set; } = new();
}