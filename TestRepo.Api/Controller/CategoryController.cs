using Microsoft.AspNetCore.Mvc;
using TetPee.Service.Category;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class CategoryController: ControllerBase
{
    private readonly IService _categoryService;
    public  CategoryController(IService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(Request.CategoryRequest categoryRequest)
    {
        var newCategory =await  _categoryService.CreateCategory(categoryRequest);
        return Ok(newCategory);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var  categories = await _categoryService.GetCategories();
        return Ok(categories);
    }
    [HttpGet("{parentId}")]
    public async Task<IActionResult>  GetChildrenByParentId(Guid parentId)
    {
        var  categories = await _categoryService.GetChildrenByParentId(parentId);
        return Ok(categories);
    }
}