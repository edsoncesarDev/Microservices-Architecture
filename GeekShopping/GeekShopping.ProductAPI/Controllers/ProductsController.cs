using GeekShopping.ProductAPI.Dto;
using GeekShopping.ProductAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRespository _product;

    public ProductsController(IProductRespository product)
    {
        _product = product;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _product.GetAllProducts();

        if (products.Count == 0)
        {
            return NoContent();
        }

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _product.GetProductById(id);

        if (product is null) 
        {
            return NoContent();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDto inputModel)
    {
        var product = await _product.AddProduct(inputModel);

        if (product is null)
        {
            return BadRequest();
        }

        return Ok(product);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductDto inputModel)
    {
        var product = await _product.UpdateProduct(inputModel);
        
        if (product is null) 
        { 
            return BadRequest(); 
        }

        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        bool deleteProduct = await _product.DeleteProduct(id);

        if (!deleteProduct) 
        {
            return BadRequest();
        }

        return Ok();
    }
}
