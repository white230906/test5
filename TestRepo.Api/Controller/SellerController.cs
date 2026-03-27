using Microsoft.AspNetCore.Mvc;
using TetPee.Repository;
using TetPee.Service.Seller;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class SellerController: ControllerBase
{
    private readonly IService _sellerService;

    public SellerController(IService sellerService)
    {
        _sellerService = sellerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSeller(Request.SellerRequest requestSeller)
    {
        var result = await _sellerService.CreateSeller(requestSeller);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetSellers(string? searchTerm, int pageIndex = 1, int pageSize = 10)
    {
        var result = await _sellerService.GetSellers(searchTerm, pageIndex, pageSize);
        return Ok(result);
    }
}