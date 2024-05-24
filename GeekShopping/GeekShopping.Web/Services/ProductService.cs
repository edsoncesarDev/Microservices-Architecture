using GeekShopping.Web.Models;
using GeekShopping.Web.Resource;
using GeekShopping.Web.Services.Interfaces;
using System.Net.Http;

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
        var response = await _httpClient.SendAsync(SetAuthorization(HttpMethod.Get));
        ValidateHttpStatus(response);

        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> GetProductById(int id)
    {
        var response = await _httpClient.GetAsync($"{BasePath}/{id}");

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> AddProduct(ProductModel product)
    {
        var response = await _httpClient.PostAsJson(BasePath, product);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        var response = await _httpClient.PutAsJson(BasePath, product);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<bool> DeleteProductById(int id)
    {
        var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");

        ValidateHttpStatus(response);

        return response.IsSuccessStatusCode;
    }

    private HttpRequestMessage SetAuthorization(HttpMethod method)
    {
        var request = new HttpRequestMessage(method, BasePath);
        request.Headers.Add("Authorization", $"bearer {_session.GetUserSession().Token}");
        return request;
    }
   
    private void ValidateHttpStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Something went wrong when calling the API : {response.ReasonPhrase}");
        }
    }
}
