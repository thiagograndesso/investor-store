using AutoMapper;
using InvestorStore.Catalog.Application.Dtos;
using InvestorStore.Catalog.Domain;

namespace InvestorStore.Catalog.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<ProductDto, Product>()
                .ConstructUsing(p => new Product(p.Name, p.Description, p.Image, p.IsActive, 
                    p.CreatedAt, p.Price, p.CategoryId, new Dimensions(p.Height, p.Width, p.Depth)));
            
            CreateMap<CategoryDto, Category>();
        }
    }
}