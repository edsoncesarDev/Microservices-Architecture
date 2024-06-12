using GeekShopping.CartAPI.Dto;
using GeekShopping.CartAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartsController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("GetCartByUserId/{userId:int}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            var cart = await _cartRepository.FindCartByUserId(userId);

            if(cart is null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("SaveCart")]
        public async Task<IActionResult> SaveCart(CartDto model)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(model);

            if(cart is null)
            {
                return BadRequest();
            }

            return Ok(cart);
        }

        [HttpPost("UpdateCart")]
        public async Task<IActionResult> UpdateCart(CartDto model)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(model);

            if (cart is null)
            {
                return BadRequest();
            }

            return Ok(cart);
        }

        [HttpDelete("RemoveCart/{cartId:int}")]
        public async Task<IActionResult> RemoveCart(int cartId)
        {
            var cart = await _cartRepository.RemoveFromCart(cartId);

            if (!cart)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("ClearCart/{userId:int}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var cart = await _cartRepository.ClearCart(userId);

            if (!cart)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDto model)
        {
            var status = await _cartRepository.ApplyCoupon(model.CartHeader!.UserId, model.CartHeader.CouponCode!);

            if (!status)
            {
                return NotFound();
            }

            return Ok(status);
        }

        [HttpDelete("RemoveCoupon/{userId:int}")]
        public async Task<IActionResult> RemoveCoupon(int userId)
        {
            var status = await _cartRepository.RemoveCoupon(userId);

            if (!status)
            {
                return NotFound();
            }

            return Ok(status);
        }
    }
}
