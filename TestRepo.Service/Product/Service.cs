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
        var nameQuery = _dbContext.Products.Where
            (x => x.Name.Trim().ToLower() == productRequest.Name.Trim().ToLower());
        var existName = await nameQuery.AnyAsync();
        if (existName)
        {   
            throw new Exception("Product name already exists");
        }
        var sellerQuery = _dbContext.Products.Where(x => x.SellerId == productRequest.SellerId);
        var exitSeller = await sellerQuery.AnyAsync();
        if (!exitSeller)
        {
            throw new Exception("Seller not found");
        }

        var newProduct = new Repository.Entity.Product()
        {
            Name = productRequest.Name,
            SellerId = productRequest.SellerId,
            Price = productRequest.Price,
        };
        _dbContext.Add(newProduct);
        await _dbContext.SaveChangesAsync();
        if (productRequest.CategoryId != null && productRequest.CategoryId.Count > 0)
        {
            var productCateList = productRequest.CategoryId.Select(x => new Repository.Entity.ProductCategory()
            {
                CategoryId = x,
                ProductId = newProduct.Id
            });
            _dbContext.AddRange(productCateList);
            await _dbContext.SaveChangesAsync();
        }

        return Response.Message.Created;
    }

    public Task<string> UpdateProduct(Request.ProductRequest productRequest)
    {
        throw new NotImplementedException();
    }
}
//Flow create Product
//1. Check name có exist không
//2. Check seller có exist không
//3. Tạo mới Product -> add -> saveChange
//4. Check CategoryIds có tồn tại không -> nếu có thì -> map sang ProductCategory -> CategoryId = Id | ProductId = request.Id
//5. => map sang rồi AddRange -> SaveChange