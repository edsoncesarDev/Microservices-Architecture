
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using System.Text.Json;

namespace GeekShopping.Web.Services;

public class SessionUser : ISessionUser
{
    private readonly IHttpContextAccessor _httpContext;
    private string _key = "LoggedUser";

    public SessionUser(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public void CreateUserSession(UserModel userModel)
    {
        var json = JsonSerializer.Serialize(userModel);
        _httpContext.HttpContext?.Session.SetString(_key, json);
    }

    public UserModel GetUserSession()
    {
        var user = JsonSerializer.Deserialize<UserModel>(_httpContext.HttpContext?.Session.Get(_key))!;

        if(user is not null)
        {
            return user;
        }

        return null!;
    }

    public void RemoveUserSession()
    {
        _httpContext.HttpContext?.Session.Remove(_key);
    }
}
