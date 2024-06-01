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

        [HttpGet("GetCartByUserId/{id:int}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            var cart = await _cartRepository.FindCartByUserId(userId);

            if(cart is null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("SaveOrUpdate")]
        public async Task<IActionResult> SaveOrUpdateCart(CartDto model)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(model);

            if(cart is null)
            {
                return BadRequest();
            }

            return Ok(cart);
        }

        [HttpDelete("RemoveCart/{id:int}")]
        public async Task<IActionResult> RemoveCart(int userId)
        {
            var cart = await _cartRepository.RemoveFromCart(userId);

            if (!cart)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("ClearCart/{id:int}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var cart = await _cartRepository.ClearCart(userId);

            if (!cart)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
