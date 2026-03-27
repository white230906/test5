using Microsoft.EntityFrameworkCore;
using TestRepo.Api.Extensions;
using TestRepo.Api.Middlewares;
using TetPee.Repository;

using JwtService = TetPee.Service.JwtService;
using CategoryService = TetPee.Service.Category;
using UserService = TetPee.Service.User;
using IdentityService = TetPee.Service.Identity;
using SellerService = TetPee.Service.Seller;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(  
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

    
builder.Services.AddJwtServices(builder.Configuration);


builder.Services.AddSwaggerServices();
builder.Services.AddScoped<SellerService.IService, SellerService.Service>();
builder.Services.AddScoped<UserService.IService, UserService.Service>();
builder.Services.AddScoped<JwtService.IService, JwtService.Service>();
builder.Services.AddScoped<IdentityService.IService, IdentityService.Service>();
builder.Services.AddScoped<CategoryService.IService, CategoryService.Service>();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerAPI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();