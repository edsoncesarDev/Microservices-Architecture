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
        BasePath = $"{_configuration["ServicesURL:IdentityUserAPI"]!}/api/v1/Users/Login";
    }

    public async Task<UserModel> LoginUser(UserLoginModel userModel)
    {
        var response = await _httpClient.PostAsJson(BasePath, userModel);

        ValidateHttpStatus(response);

        var user = await response.ReadContentAs<UserModel>();

        _sessionUser.CreateUserSession(user);

        return user;
    }

    private void ValidateHttpStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Something went wrong when calling the API : {response.ReasonPhrase}");
        }
    }
}
