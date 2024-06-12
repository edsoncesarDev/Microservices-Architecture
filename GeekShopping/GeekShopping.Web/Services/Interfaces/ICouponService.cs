using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface ICouponService
{
    Task<CouponModel> GetCouponAsync(string code);
}
