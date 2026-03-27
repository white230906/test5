using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.Service.Product;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreateProduct(Request.ProductRequest productRequest)
    {
        var nameQuery =  _dbContext.Products.Where(x => x.Name == productRequest.Name);
        var existName = await  nameQuery.AnyAsync();
        if (existName)
        {
            throw new Exception("Product name already exists");
        }
        var sellerQuery =  _dbContext.Products.Where(x => x.SellerId == productRequest.SellerId);
        var existSeller = await sellerQuery.AnyAsync();
        if (!existSeller)
        {
            throw new Exception("Seller not found");
        }
        var newProduct = new Repository.Entity.Product()
        {
            Name = productRequest.Name,
            Price = productRequest.Price,
            SellerId = productRequest.SellerId
        };
        _dbContext.Add(newProduct);
        await _dbContext.SaveChangesAsync();
        if (productRequest.CategoryIds != null && productRequest.CategoryIds.Count > 0)
        {
            var productCateList = productRequest.CategoryIds.Select(x => new Repository.Entity.ProductCategory()
            {
                CategoryId = x,
                ProductId = newProduct.Id
            });
            _dbContext.AddRange(productCateList);
            await _dbContext.SaveChangesAsync();
        }

        return Response.Message.Created;
    }
}