namespace TetPee.Service.Category;

public interface IService
{
    public Task<string> CreateCategory(Request.CategoryRequest categoryRequest);
    public Task<List<Response.CategoryResponse>> GetCategories();
    public Task<List<Response.CategoryResponse>> GetChildrenByParentId(Guid parentId);
}