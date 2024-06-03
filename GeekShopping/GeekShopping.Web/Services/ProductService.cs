using GeekShopping.Web.Models;
using GeekShopping.Web.Resource;
using GeekShopping.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services;

public sealed class ProductService : IProductService
{
    private HttpClient _httpClient;
    private string _basePath;
    private readonly IConfiguration _configuration;
    private readonly ISessionUser _session;

    public ProductService(HttpClient httpClient, IConfiguration configuration, ISessionUser session)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _basePath = _configuration["ServicesURL:ProductAPI"]!;
        _session = session;
    }

    public async Task<List<ProductModel>> GetAllProducts()
    {
        SetAuthorization();

        var response = await _httpClient.GetAsync(_basePath);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> GetProductById(int id)
    {
        SetAuthorization();

        var response = await _httpClient.GetAsync($"{_basePath}/{id}");

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> AddProduct(ProductModel product)
    {
        SetAuthorization();

        var response = await _httpClient.PostAsJson(_basePath, product);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        SetAuthorization();

        var response = await _httpClient.PutAsJson(_basePath, product);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<bool> DeleteProductById(int id)
    {
        SetAuthorization();

        var response = await _httpClient.DeleteAsync($"{_basePath}/{id}");

        ValidateHttpStatus(response);

        return response.IsSuccessStatusCode;
    }

    private void SetAuthorization()
    {
        if(_session.GetUserSession() != null)
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
