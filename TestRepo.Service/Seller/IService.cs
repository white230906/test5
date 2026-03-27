namespace TetPee.Service.Seller;

public interface IService
{
   public Task<string> CreateSeller (Request.SellerRequest requestSeller);
   public Task<Base.Response.PageResult<Response.SellerResponse>> GetSellers (string? searchTerm, int pageIndex, int pageSize);
}