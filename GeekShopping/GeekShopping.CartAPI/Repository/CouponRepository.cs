using GeekShopping.CartAPI.Dto;
using GeekShopping.CartAPI.Repository.Interface;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.CartAPI.Repository;

public sealed class CouponRepository : ICouponRespository
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IConfiguration _configuration;
    private string _basePath;

    public CouponRepository(HttpClient httpClient, IHttpContextAccessor httpContext, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpContext = httpContext;
        _configuration = configuration;
        _basePath = _configuration["ServicesURL:CouponAPI"]!;
    }

    public async Task<CouponDto> GetCouponByCodeAsync(string couponCode)
    {
        SetAuthorization();

        var response = await _httpClient.GetAsync($"{_basePath}/{couponCode}");

        ValidateHttpStatus(response);

        var content = await response.Content.ReadAsStringAsync();
        
        var teste = JsonSerializer.Deserialize<CouponDto>(content)!;

        return teste;
    }

    private void SetAuthorization()
    {
        if (_httpContext.HttpContext!.Request.Headers.Authorization!.ToString().Split(" ")[1] != null)
        {
            string token = _httpContext.HttpContext!.Request.Headers.Authorization!.ToString().Split(" ")[1];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

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
