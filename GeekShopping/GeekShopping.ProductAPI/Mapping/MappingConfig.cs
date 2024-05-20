using AutoMapper;
using GeekShopping.ProductAPI.Domain.Entities;
using GeekShopping.ProductAPI.Dto;

namespace GeekShopping.ProductAPI.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
