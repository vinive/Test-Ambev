using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly DefaultContext _context;

        public SalesController(DefaultContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _context.Sales
            .Include(s => s.Items)
            .ToListAsync();

            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(Guid id)

        {
            var sale = await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return NotFound();

            return Ok(sale);

        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] SaleCreateDto saleDto)
        {
            try
            {
                var sale = new Sale
                {
                    Customer = saleDto.Customer,
                    Branch = saleDto.Branch,
                };

                var groupedItems = saleDto.Items
                    .GroupBy(i => i.Product)
                    .Select(g =>
                    {
                        var unitPrices = g.Select(i => i.UnitPrice).Distinct().ToList();

                        if (unitPrices.Count > 1)
                            throw new ArgumentException($"Produto '{g.Key}' possui múltiplos preços unitários diferentes na lista informada.");

                        return new
                        {
                            Product = g.Key,
                            TotalQuantity = g.Sum(i => i.Quantity),
                            UnitPrice = unitPrices.First()
                        };
                    });

                foreach (var itemGroup in groupedItems)
                {
                    var item = new SaleItem
                    {
                        Product = itemGroup.Product,
                        Quantity = itemGroup.TotalQuantity,
                        UnitPrice = itemGroup.UnitPrice
                    };

                    sale.AddItem(item);
                }

                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();

                Console.WriteLine($"[Evento] SaleCreated: Venda #{sale.Id} criada para o cliente {sale.Customer}");

                return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, sale);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"[Erro] {ex.Message}");
                return StatusCode(500, new { Message = "Ocorreu um erro interno no servidor." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] SaleUpdateDto saleDto)
        {
            var sale = await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return NotFound();

            sale.Customer = saleDto.Customer;
            sale.Branch = saleDto.Branch;

            // Limpa os itens antigos e adiciona os novos
            _context.SaleItems.RemoveRange(sale.Items);

            sale.Items = saleDto.Items.Select(i => new SaleItem
            {
                Product = i.Product,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();

            await _context.SaveChangesAsync();

            Console.WriteLine($"[Evento] SaleModified: Venda #{sale.Id} modificada para o cliente {sale.Customer}");

            return NoContent();
        }

        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return NotFound();

            if (sale.IsCancelled)
                return BadRequest("A venda já está cancelada.");

            sale.CancelSale();
            await _context.SaveChangesAsync();

            Console.WriteLine($"[Evento] SaleCancelled: Venda #{sale.Id} cancelada para o cliente {sale.Customer}");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(Guid id)
        {
            var sale = await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return NotFound();

            // Remove os itens filhos primeiro, se necessário
            _context.SaleItems.RemoveRange(sale.Items);

            // Remove a venda
            _context.Sales.Remove(sale);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}