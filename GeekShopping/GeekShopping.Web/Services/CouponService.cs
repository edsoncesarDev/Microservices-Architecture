using GeekShopping.Web.Models;
using GeekShopping.Web.Resource;
using GeekShopping.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services;

public sealed class CouponService : ICouponService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ISessionUser _session;
    private string _basePath;

    public CouponService(HttpClient httpClient, IConfiguration configuration, ISessionUser session)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _basePath = _configuration["ServicesURL:CouponAPI"]!;
        _session = session;
    }
    public async Task<CouponModel> GetCouponAsync(string code)
    {
        SetAuthorization();

        var response = await _httpClient.GetAsync($"{_basePath}/{code}");

        ValidateHttpStatus(response);

        return await response.ReadContentAs<CouponModel>();
    }

    private void SetAuthorization()
    {
        if (_session.GetUserSession() != null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _session.GetUserSession().Token);

        }
    }

    private void ValidateHttpStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Something went wrong when calling the API : {response.ReasonPhrase}");
        }
    }
}
