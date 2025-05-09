﻿using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductModel>> GetAllProducts();
    Task<ProductModel> GetProductById(int id);
    Task<ProductModel> AddProduct(ProductModel product);
    Task<ProductModel> UpdateProduct(ProductModel product);
    Task<bool> DeleteProductById(int id);
}
