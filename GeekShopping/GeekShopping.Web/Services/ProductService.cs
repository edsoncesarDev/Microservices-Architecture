using GeekShopping.Web.Models;
using GeekShopping.Web.Resource;
using GeekShopping.Web.Services.Interfaces;
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
        var response = await _httpClient.SendAsync(SetAuthorization(HttpMethod.Get, BasePath));

        ValidateHttpStatus(response);

        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> GetProductById(int id)
    {
        var response = await _httpClient.SendAsync(SetAuthorization(HttpMethod.Get, $"{BasePath}/{id}"));

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> AddProduct(ProductModel product)
    {
        var response = await _httpClient.SendAsync(SetAuthorization(HttpMethod.Post, BasePath, product));

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        var response = await _httpClient.SendAsync(SetAuthorization(HttpMethod.Put, BasePath, product));

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<bool> DeleteProductById(int id)
    {
        var response = await _httpClient.SendAsync(SetAuthorization(HttpMethod.Delete, $"{BasePath}/{id}"));

        ValidateHttpStatus(response);

        return response.IsSuccessStatusCode;
    }

    private HttpRequestMessage SetAuthorization(HttpMethod method, string basePath, dynamic data = null!)
    {
        var request = new HttpRequestMessage(method, basePath);

        if(method == HttpMethod.Post || method == HttpMethod.Put)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
        }

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
