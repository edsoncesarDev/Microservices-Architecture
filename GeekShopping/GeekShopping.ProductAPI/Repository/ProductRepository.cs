using AutoMapper;
using GeekShopping.ProductAPI.Domain.Entities;
using GeekShopping.ProductAPI.Dto;
using GeekShopping.ProductAPI.Infrastructure.Context;
using GeekShopping.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository;

public sealed class ProductRepository : IProductRespository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetAllProducts()
    {
        var products =  await _context.Products.AsNoTracking().ToListAsync();

        if (products is null)
        {
            return null!;
        }

        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductById(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if(product is null)
        {
            return null!;
        }

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> AddProduct(ProductDto productDto)
    {
        var product = new Product(productDto.Name, productDto.Price, productDto.Description!, productDto.Category!, productDto.ImageURL!);

        await _context.Products.AddAsync(product);

        await _context.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
        
    }

    public async Task<ProductDto> UpdateProduct(ProductDto productDto)
    {
        var product = GetModelProductById(productDto.Id);

        if (product is null)
        {
            return null!;
        }

        product.ImplementProduct(productDto.Name, productDto.Price, productDto.Description!, productDto.Category!, productDto.ImageURL!);

        _context.Products.Update(product);

        await _context.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var product = GetModelProductById(id);

        if(product is null)
        {
            return false;
        }

        _context.Products.Remove(product);

        return await _context.SaveChangesAsync() > 0;
    }

    private Product? GetModelProductById(int id)
    {
       return _context.Products.FirstOrDefault(x => x.Id == id);
       
    }
}
