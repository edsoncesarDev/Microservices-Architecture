using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface ISessionUser
{
    void CreateUserSession(UserModel userModel);
    void RemoveUserSession();
    UserModel GetUserSession();
}
