using GeekShopping.IdentityUserAPI.Dto;

namespace GeekShopping.IdentityUserAPI.Repository.Interfaces;

public interface IUser
{
    Task<UserDto> RegisterUser(UserDto userDto);
    UserTokenDto GenerateTokenUser(UserDto userDto);
    Task<UserTokenDto> Login(UserLoginDto loginDto);
}
