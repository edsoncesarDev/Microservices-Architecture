using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces
{
    public interface IIdentityUser
    {
        Task<UserModel> LoginUser(UserLoginModel userModel);
        Task<UserRegisterModel> RegisterUser(UserRegisterModel userModel);
    }
}
