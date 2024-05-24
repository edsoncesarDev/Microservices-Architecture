using GeekShopping.Web.Filters;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

[LoggedUsers]
[AuthorizeAdmin]
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

    public async Task<IActionResult> ViewUpdate(int id)
    {
       var product = await _productService.GetProductById(id);

        if(product != null)
        {
            return View(product);
        }
        
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.UpdateProduct(model);

            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        return View(model);
    }

    [Authorize]
    public async Task<IActionResult> ViewDelete(int id)
    {
        var product = await _productService.GetProductById(id);

        if (product != null)
        {
            return View(product);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ProductModel model)
    {
        var response = await _productService.DeleteProductById(model.Id);

        if (response)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
}
