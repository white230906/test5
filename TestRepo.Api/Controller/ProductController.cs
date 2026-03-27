using Microsoft.AspNetCore.Mvc;
using TetPee.Service.Product;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class ProductController: ControllerBase
{
    private readonly IService _productService;
    public ProductController(IService productService)
    {
        _productService = productService;
    }

    [HttpPost("")]
    public async Task<IActionResult> AddProductCategory(Request.ProductRequest productRequest)
    {
        var newProduct = await _productService.CreateProduct(productRequest);
        return Ok(newProduct);
    }
}