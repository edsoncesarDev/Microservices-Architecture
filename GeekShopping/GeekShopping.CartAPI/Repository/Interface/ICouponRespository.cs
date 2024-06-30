using GeekShopping.CartAPI.Dto;

namespace GeekShopping.CartAPI.Repository.Interface;

public interface ICouponRespository
{
    Task<CouponDto> GetCouponByCodeAsync(string couponCode);
}
