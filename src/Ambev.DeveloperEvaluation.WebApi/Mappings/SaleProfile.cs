using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Dtos;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        // Mapeamento de DTO para Entidade
        CreateMap<CreateSaleDto, Sale>();
        CreateMap<SaleItemCreateDto, SaleItem>();

           // Mapeamento de DTO para Entidade
        CreateMap<SaleUpdateDto, Sale>();
        CreateMap<SaleItemUpdateDto, SaleItem>();
        
        // (opcional) Mapeamento de Entidade para DTO, se for usar para retornos
        CreateMap<Sale, SaleDto>();
        CreateMap<SaleItem, SaleItemDto>();
    }
}