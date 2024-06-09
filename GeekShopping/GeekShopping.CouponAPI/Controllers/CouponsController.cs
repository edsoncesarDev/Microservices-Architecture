using GeekShopping.CouponAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponsController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [Authorize]
        [HttpGet("{couponCode}")]
        public async Task<IActionResult> GetCouponCode(string couponCode)
        {
            var coupon = await _couponRepository.GetCouponByCodeAsync(couponCode);

            if(coupon is null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }
    }
}
