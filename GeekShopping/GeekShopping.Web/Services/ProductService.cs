using GeekShopping.Web.Models;
using GeekShopping.Web.Resource;
using GeekShopping.Web.Services.Interfaces;

namespace GeekShopping.Web.Services;

public sealed class ProductService : IProductService
{
    private HttpClient _httpClient;
    private string BasePath = "api/v1/products";

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductModel>> GetAllProducts()
    {
        var response = await _httpClient.GetAsync(BasePath);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<List<ProductModel>>(response);
    }

    public async Task<ProductModel> GetProductById(int id)
    {
        var response = await _httpClient.GetAsync($"{BasePath}/{id}");

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>(response);
    }

    public async Task<ProductModel> AddProduct(ProductModel product)
    {
        var response = await _httpClient.PostAsJson(BasePath, product);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>(response);
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        var response = await _httpClient.PutAsJson(BasePath, product);

        ValidateHttpStatus(response);

        return await response.ReadContentAs<ProductModel>(response);
    }

    public async Task<bool> DeleteProductById(int id)
    {
        var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");

        ValidateHttpStatus(response);

        return response.IsSuccessStatusCode;
    }

    private void ValidateHttpStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Something went wrong when calling the API : {response.ReasonPhrase}");
        }
    }
}
