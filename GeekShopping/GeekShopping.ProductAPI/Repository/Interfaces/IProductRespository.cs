using GeekShopping.ProductAPI.Dto;

namespace GeekShopping.ProductAPI.Repository.Interfaces;

public interface IProductRespository
{
    Task<List<ProductDto>> GetAllProducts();
    Task<ProductDto> GetProductById(int id);
    Task<ProductDto> AddProduct(ProductDto productDto);
    Task<ProductDto> UpdateProduct(ProductDto productDto);
    Task<bool> DeleteProduct(int id);
}
