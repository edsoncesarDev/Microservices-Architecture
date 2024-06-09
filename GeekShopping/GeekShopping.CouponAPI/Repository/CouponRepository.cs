using AutoMapper;
using GeekShopping.CouponAPI.Dto;
using GeekShopping.CouponAPI.Infrastructure.Context;
using GeekShopping.CouponAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Repository;

public sealed class CouponRepository : ICouponRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CouponRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CouponDto> GetCouponByCodeAsync(string couponCode)
    {
        var coupon = await _context.Coupons.AsNoTracking().FirstOrDefaultAsync(x => x.CouponCode == couponCode);

        if(coupon is null)
        {
            return null!;
        }

        return _mapper.Map<CouponDto>(coupon);
    }
}
