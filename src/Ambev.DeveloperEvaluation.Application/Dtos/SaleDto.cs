namespace Ambev.DeveloperEvaluation.Application.Dtos
{
    public class SaleDto
    {
        public Guid Id { get; set; } // Opcional no POST, obrigatório no PUT/GET
        public int SaleNumber { get; set; } // Pode ser ignorado no POST, gerado internamente
        public DateTime Date { get; set; }
        public string Customer { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public List<SaleItemDto> Items { get; set; } = new();
        public bool IsCancelled { get; set; } // só leitura (opcional no POST)
    }
}