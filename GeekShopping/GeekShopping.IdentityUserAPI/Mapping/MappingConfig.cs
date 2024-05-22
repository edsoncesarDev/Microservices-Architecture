
using AutoMapper;
using GeekShopping.IdentityUserAPI.Domain;
using GeekShopping.IdentityUserAPI.Dto;

namespace GeekShopping.ProductAPI.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
