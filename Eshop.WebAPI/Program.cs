using Npgsql;
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

var builder = WebApplication.CreateBuilder(args);

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? throw new InvalidOperationException("Database connection string 'DATABASE_URL' not found.");
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new InvalidOperationException("JWT Key is not set.");
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT Issuer is not set.");


// Connection string for administrative tasks with 'mostafizurrahman'
var adminConnectionString = "Host=localhost;Port=5432;Username=mostafizurrahman;Password=mostafizurrahmansecret;Database=postgres;"; // Adjust superuser details accordingly

// Ensure the database is created before configuring the DbContext
EnsureDatabaseCreated(adminConnectionString, "eshop", "test_admin", "testadminsecret");


// Configure services...
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("PgDbConnection"));
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<EshopDbContext>(
    options => options.UseNpgsql(dataSource)
                      .UseSnakeCaseNamingConvention()
                      .AddInterceptors(new TimeStampInterceptor()));

// Configure Authentication...
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Secrets:JwtKey"])),
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Secrets:Issuer"]
        };
    });

// Service registration
builder.Services.AddScoped<IUserRepository, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();  // Note: AuthService is registered twice. Intentional?
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

// Configure middlewares...
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();



// Function to ensure the database is created
static void EnsureDatabaseCreated(string adminConnectionString, string dbName, string role, string password)
{
    using var conn = new NpgsqlConnection(adminConnectionString);
    conn.Open();

    // Check if the role exists
    var roleExists = new NpgsqlCommand($"SELECT 1 FROM pg_roles WHERE rolname='{role}'", conn).ExecuteScalar() != null;
    if (!roleExists)
    {
        // Create role if it does not exist
        new NpgsqlCommand($"CREATE ROLE {role} WITH LOGIN PASSWORD '{password}' CREATEDB;", conn).ExecuteNonQuery();
    }

    // Check if the database exists
    var dbExists = new NpgsqlCommand($"SELECT 1 FROM pg_database WHERE datname='{dbName}'", conn).ExecuteScalar() != null;
    if (!dbExists)
    {
        // Create database with the role as the owner
        new NpgsqlCommand($"CREATE DATABASE {dbName} WITH OWNER = {role};", conn).ExecuteNonQuery();
    }

    conn.Close();
}