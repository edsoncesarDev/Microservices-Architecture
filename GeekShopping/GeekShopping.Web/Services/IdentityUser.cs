using GeekShopping.Web.Models;
using GeekShopping.Web.Resource;
using GeekShopping.Web.Services.Interfaces;

namespace GeekShopping.Web.Services;

public class IdentityUser : IIdentityUser
{
    private HttpClient _httpClient;
    private readonly ISessionUser _sessionUser;
    private string BasePath;
    private readonly IConfiguration _configuration;

    public IdentityUser(HttpClient httpClient, ISessionUser sessionUser, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _sessionUser = sessionUser;
        _configuration = configuration;
        BasePath = _configuration["ServicesURL:IdentityUserAPI"]!;
    }

    public async Task<UserModel> LoginUser(UserLoginModel userModel)
    {
        var response = await _httpClient.PostAsJson($"{BasePath}/login", userModel);

        ValidateHttpStatus(response);

        var user = await response.ReadContentAs<UserModel>();

        _sessionUser.CreateUserSession(user);

        return user;
    }

    public async Task<UserRegisterModel> RegisterUser(UserRegisterModel userModel)
    {
        var response = await _httpClient.PostAsJson($"{BasePath}/register", userModel);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<UserRegisterModel>();
    }

    private void ValidateHttpStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Something went wrong when calling the API : {response.ReasonPhrase}");
        }
    }

    
}
