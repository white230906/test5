namespace TetPee.Service.Product;

public interface IService
{
    public Task<string> CreateProduct(Request.ProductRequest productRequest);
    public Task<string> UpdateProduct(Request.ProductRequest productRequest);
}