using AutoMapper;
using GeekShopping.CartAPI.Domain.Entities;
using GeekShopping.CartAPI.Dto;

namespace GeekShopping.CartAPI.Mapping;

public sealed class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
        CreateMap<CartDetail, CartDetailDto>().ReverseMap();
        CreateMap<Cart, CartDto>().ReverseMap();
    }
}
