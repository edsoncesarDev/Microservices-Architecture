using GeekShopping.CouponAPI.Dto;

namespace GeekShopping.CouponAPI.Repository.Interfaces;

public interface ICouponRepository
{
    Task<CouponDto> GetCouponByCodeAsync(string couponCode);
}
