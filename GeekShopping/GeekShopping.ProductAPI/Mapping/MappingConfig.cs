using AutoMapper;
using GeekShopping.ProductAPI.Domain.Entities;
using GeekShopping.ProductAPI.Dto;

namespace GeekShopping.ProductAPI.Mapping;

public sealed class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
