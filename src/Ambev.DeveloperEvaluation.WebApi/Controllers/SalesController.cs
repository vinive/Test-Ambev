using Ambev.DeveloperEvaluation.Application.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;

    public SalesController(ISaleService saleService, IMapper mapper)
    {
        _saleService = saleService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleDto saleCreateDto)
    {
        var sale = _mapper.Map<Sale>(saleCreateDto);
        var created = await _saleService.CreateSaleAsync(sale);
        var result = _mapper.Map<SaleDto>(created);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _saleService.GetAllSalesAsync();
        var result = _mapper.Map<IEnumerable<SaleDto>>(sales);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sale = await _saleService.GetSaleByIdAsync(id);
        if (sale == null) return NotFound();
        var result = _mapper.Map<SaleDto>(sale);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] SaleUpdateDto saleUpdateDto)
    {
        var sale = _mapper.Map<Sale>(saleUpdateDto);
        var updated = await _saleService.UpdateSaleAsync(id, sale);
        var result = _mapper.Map<SaleDto>(updated);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        await _saleService.CancelSaleAsync(id);
        return NoContent();
    }
}
