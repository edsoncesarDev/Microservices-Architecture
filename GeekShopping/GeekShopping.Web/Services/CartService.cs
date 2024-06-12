using GeekShopping.Web.Models;
using GeekShopping.Web.Resource;
using GeekShopping.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services;

public class CartService : ICartService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ISessionUser _session;
    private string _basePath;

    public CartService(HttpClient httpClient, IConfiguration configuration, ISessionUser session)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _basePath = _configuration["ServicesURL:CartAPI"]!;
        _session = session;
    }

    public async Task<CartModel> FindCartByUserId(int userId)
    {
        SetAuthorization();

        var response = await _httpClient.GetAsync($"{_basePath}/GetCartByUserId/{userId}");

        ValidateHttpStatus(response);

        return await response.ReadContentAs<CartModel>();
    }

    public async Task<CartModel> AddItemToCart(CartModel cart)
    {
        SetAuthorization();

        var response = await _httpClient.PostAsJson($"{_basePath}/SaveCart", cart);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<CartModel>();
    }

    public async Task<CartModel> UpdateCart(CartModel cart)
    {
        SetAuthorization();

        var response = await _httpClient.PutAsJson($"{_basePath}/UpdateCart", cart);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<CartModel>();
    }

    public async Task<bool> RemoveFromCart(int cartId)
    {
        SetAuthorization();

        var response = await _httpClient.DeleteAsync($"{_basePath}/RemoveCart/{cartId}");

        ValidateHttpStatus(response);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ApplyCoupon(CartModel cart)
    {
        SetAuthorization();

        var response = await _httpClient.PostAsJson($"{_basePath}/ApplyCoupon", cart);

        ValidateHttpStatus(response);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveCoupon(int userId)
    {
        SetAuthorization();

        var response = await _httpClient.DeleteAsync($"{_basePath}/RemoveCoupon/{userId}");

        ValidateHttpStatus(response);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ClearCart(int userId)
    {
        SetAuthorization();

        var response = await _httpClient.DeleteAsync($"{_basePath}/ClearCart/{userId}");

        ValidateHttpStatus(response);

        return response.IsSuccessStatusCode;
    }

    public async Task<CartModel> Checkout(CartHeaderModel cartHeader)
    {
        throw new NotImplementedException();
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
