using System.Text;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Eshop.Core.src.RepoAbstraction;
using Eshop.Service.src.Service;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.WebApi.src.Data;
using Eshop.WebApi.src.middleware;
using Eshop.WebApi.src.Repo;
using Eshop.WebApi.src.Service;
using Eshop.WebAPI.src.Service;
using Eshop.Core.src.RepositoryAbstraction;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
Env.Load();
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? throw new InvalidOperationException("Database connection string 'DATABASE_URL' not found.");
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new InvalidOperationException("JWT Key is not set.");
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT Issuer is not set.");

// Configure services...
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddDbContext<EshopDbContext>(
    options => options.UseNpgsql(databaseUrl)
                      .UseSnakeCaseNamingConvention()
                      .AddInterceptors(new TimeStampInterceptor()));

// Configure Authentication...
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer
        };
    });

// Service registration
builder.Services.AddScoped<IUserRepository, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IProductLineRepository, ProductLineRepository>();
builder.Services.AddScoped<IProductLineService, ProductLineService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductColorRepository, ProductColorRepository>();
builder.Services.AddScoped<IProductColorService, ProductColorService>();
builder.Services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
builder.Services.AddScoped<IProductSizeService, ProductSizeService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ExceptionHandlerMiddleware>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Apply pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EshopDbContext>();
    dbContext.Database.Migrate();
}

// Configure middlewares...
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

