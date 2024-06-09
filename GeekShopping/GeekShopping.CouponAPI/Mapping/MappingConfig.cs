using AutoMapper;
using GeekShopping.CouponAPI.Domain.Entities;
using GeekShopping.CouponAPI.Dto;

namespace GeekShopping.CouponAPI.Mapping;

public sealed class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Coupon, CouponDto>().ReverseMap();
    }
}
