using AutoMapper;
using GeekShopping.ProductAPI.Domain.Entities;
using GeekShopping.ProductAPI.Dto;
using GeekShopping.ProductAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Roles;

namespace GeekShopping.ProductAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _product;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> product, IMapper mapper)
    {
        _product = product;
        _mapper = mapper;
    }

    //[Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _product.GetAll();

        if (products.Count == 0)
        {
            return NoContent();
        }

        return Ok(_mapper.Map<List<ProductDto>>(products));
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _product.FindById(id);

        if (product is null) 
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto inputModel)
    {
        var product = await _product.Create(_mapper.Map<Product>(inputModel));

        if (product is null)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductDto inputModel)
    {
        var product = await _product.Update(_mapper.Map<Product>(inputModel));
        
        if (product is null) 
        { 
            return BadRequest(); 
        }

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [Authorize(Roles = AuthorizeRole.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        bool deleteProduct = await _product.Delete(id);

        if (!deleteProduct) 
        {
            return BadRequest();
        }

        return Ok();
    }
}
