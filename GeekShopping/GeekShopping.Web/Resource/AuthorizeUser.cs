using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GeekShopping.Web.Resource;

public sealed class AuthorizeUser : ILoggedUser
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMemoryCache _cache;
    private string _keyCache;

    public AuthorizeUser(IHttpContextAccessor contextAccessor, IMemoryCache cache)
    {
        _contextAccessor = contextAccessor;
        _cache = cache;
        _keyCache = $"{_contextAccessor.HttpContext?.Connection.RemoteIpAddress}{_contextAccessor.HttpContext?.Request.Headers["User-Agent"]}";
        AddAuthorizationToken();
    }

    public void AddAuthorizationToken()
    {
        var token = GetTokenLoggedUser();

        if (token is not null)
        {
            _contextAccessor.HttpContext!.Request.Headers.Authorization = token;
        }
        
    }

    public string GetTokenLoggedUser()
    {
        _cache.TryGetValue(_keyCache!, out string? Token);
        return Token!;
    }

    public void SetLoggedUser(UserModel userModel)
    {
        _cache.Set(_keyCache, userModel.Token);
    }
}
