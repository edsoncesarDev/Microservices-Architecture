using GeekShopping.Web.Models;
using GeekShopping.Web.Resource;
using GeekShopping.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.Web.Services;

public sealed class ProductService : IProductService
{
    private HttpClient _httpClient;
    private string BasePath;
    private readonly IConfiguration _configuration;
    private readonly ISessionUser _session;

    public ProductService(HttpClient httpClient, IConfiguration configuration, ISessionUser session)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        BasePath = _configuration["ServicesURL:ProductAPI"]!;
        _session = session;
    }

    public async Task<List<ProductModel>> GetAllProducts()
    {
        SetAuthorization();

        var response = await _httpClient.GetAsync(BasePath);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> GetProductById(int id)
    {
        SetAuthorization();

        var response = await _httpClient.GetAsync($"{BasePath}/{id}");

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> AddProduct(ProductModel product)
    {
        SetAuthorization();

        var response = await _httpClient.PostAsJson(BasePath, product);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        SetAuthorization();

        var response = await _httpClient.PutAsJson(BasePath, product);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<bool> DeleteProductById(int id)
    {
        SetAuthorization();

        var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");

        ValidateHttpStatus(response);

        return response.IsSuccessStatusCode;
    }

    private void SetAuthorization()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _session.GetUserSession().Token);
    }
   
    private void ValidateHttpStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Something went wrong when calling the API : {response.ReasonPhrase}");
        }
    }
}
