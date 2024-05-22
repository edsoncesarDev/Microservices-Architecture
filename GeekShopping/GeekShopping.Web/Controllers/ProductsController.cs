using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProducts();

        return View(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductById(id);

        return await Update(product);
    }

    public async Task<IActionResult> ViewCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.AddProduct(model);

            if (response != null) 
            {
                return RedirectToAction(nameof(Index));
            }
        }
        
        return View(model);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductModel model)
    {
        await _productService.UpdateProduct(model);

        return View(model);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProductById(id);

        return await Index();

    }
}
