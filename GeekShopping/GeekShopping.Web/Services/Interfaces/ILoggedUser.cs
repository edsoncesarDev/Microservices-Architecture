using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface ILoggedUser
{
    void SetLoggedUser(UserModel userModel);
    string GetTokenLoggedUser();
    void AddAuthorizationToken();
}
