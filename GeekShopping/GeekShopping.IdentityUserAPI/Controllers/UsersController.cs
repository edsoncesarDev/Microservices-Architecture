using GeekShopping.IdentityUserAPI.Dto;
using GeekShopping.IdentityUserAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Roles;

namespace GeekShopping.IdentityUserAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _user;

        public UsersController(IUser user)
        {
            _user = user;
        }

        [Authorize(Roles = AuthorizeRole.Admin)]
        [HttpGet]
        public IActionResult LoggedUser()
        {
            return Ok($"{User.Identity!.Name} - {DateTime.Now.ToString("dd/MM/yyyy")}: Token successfully validated");
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            var user = await _user.RegisterUser(userDto);

            if (user is null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto loginDto)
        {
            var token = await _user.Login(loginDto);

            if (token is null)
            {
                return BadRequest();
            }

            return Ok(token);
        }


    }
}
