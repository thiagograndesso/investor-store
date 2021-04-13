using AutoMapper;
using InvestorStore.Catalog.Application.Dtos;
using InvestorStore.Catalog.Domain;

namespace InvestorStore.Catalog.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Height, c => c.MapFrom(s => s.Dimensions.Height))
                .ForMember(d => d.Width, c => c.MapFrom(s => s.Dimensions.Width))
                .ForMember(d => d.Depth, c => c.MapFrom(s => s.Dimensions.Depth));
            
            CreateMap<Category, CategoryDto>();
        }
    }
}