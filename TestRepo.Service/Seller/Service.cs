using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.Service.Seller;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<string> CreateSeller(Request.SellerRequest requestSeller)
    {
        var emailQuery = _dbContext.Users.Where(u => u.Email == requestSeller.Email);
        var emailExist = await emailQuery.AnyAsync();
        if (emailExist)
        {
            throw new Exception("Email already exists");
        }

        var newUser = new Repository.Entity.User()
        {
            Email = requestSeller.Email,
            Password = requestSeller.Password,
            Role = "Seller"
        };
        _dbContext.Add(newUser);
          await _dbContext.SaveChangesAsync();
          var newSeller = new Repository.Entity.Seller()
          {
              UserId = newUser.Id,
              TaxCode = requestSeller.TaxCode,
              CompanyName = requestSeller.CompanyName,
              CompanyAddress = requestSeller.CompanyAddress,
          };
          _dbContext.Add(newSeller);
          await _dbContext.SaveChangesAsync();
         return Response.Message.Created;
    }

    public async Task<Base.Response.PageResult<Response.SellerResponse>> GetSellers(string? searchTerm, int pageIndex, int pageSize)
    {
        var query =  _dbContext.Sellers.Where(x => true);
        if (searchTerm != null)
        {
            query = query.Where(x => x.User.Email.Contains(searchTerm));
        }
        query = query.OrderBy(x => x.User.Email);
        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        var selectedQuery = query.Select(x => new Response.SellerResponse()
        {
            Email = x.User.Email,
            Password = x.User.Password,
            Role = x.User.Role,
        });
        var pageList = await selectedQuery.ToListAsync();
        var totalItems = pageList.Count;
        var result = new Base.Response.PageResult<Response.SellerResponse>()
        {
            Items = pageList,
            TotalItems = totalItems,
            PageSize = pageSize,
            PageIndex = pageIndex,
        };
        return  result;
    }
}