using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.Service.Category;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreateCategory(Request.CategoryRequest categoryRequest)
    {
        var existNameQuery = _dbContext.Categories.Where(c => c.Name == categoryRequest.Name);
        var existName = await existNameQuery.AnyAsync();
        if (existName)
        {
            throw new Exception("Category already exists");
        }

        var newCategory = new Repository.Entity.Category()
        {
            Name = categoryRequest.Name,
            ParentId = categoryRequest.ParentId
        };
        _dbContext.Add(newCategory);
        await _dbContext.SaveChangesAsync();
        return Response.Message.Created;
    }

    public async Task<List<Response.CategoryResponse>> GetCategories()
    {
        var query = _dbContext.Categories.Where(c => true);
        query = query.OrderBy(c => c.Name);
        var selectedQuery = query.Select(c => new Response.CategoryResponse()
        {
            Id = c.Id,
            Name = c.Name
        });
        var result = await selectedQuery.ToListAsync();
        return result;
    }

    public async Task<List<Response.CategoryResponse>> GetChildrenByParentId(Guid parentId)
    {
        var query = _dbContext.Categories.Where(c => c.ParentId == parentId);
        query = query.OrderBy(c => c.Name);
        var selectedQuery = query.Select(c => new Response.CategoryResponse()
        {
            Id = c.Id,
            Name = c.Name
        });
        var result = await selectedQuery.ToListAsync();
        return result;
    }
}