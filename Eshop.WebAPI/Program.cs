using Eshop.Core.src.RepoAbstraction;
using Eshop.Core.src.ValueObject;
using Eshop.Service.src.Service;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.WebApi.src.Data;
using Eshop.WebApi.src.middleware;
using Eshop.WebApi.src.Repo;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add all controllers
builder.Services.AddControllers();

// builder.Services.AddDbContext<EshopDbContext>(options =>
// {
//     options.UseNpgsql(builder.Configuration.GetConnectionString("PgDbConnection"));
// });


// adding db context into your app (alia)
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("PgDbConnection"));
dataSourceBuilder.MapEnum<UserRole>();
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<EshopDbContext>
(
    options =>
    options.UseNpgsql(dataSource)
    .UseSnakeCaseNamingConvention()
    // .AddInterceptors(new TimeStampInteceptor())
);



// service registration -> automatically create all instances of dependencies
builder.Services.AddScoped<IUserRepository, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<ITokenService, TokenService>();
// builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddScoped<ExceptionHandlerMiddleware>();
// builder.Services.AddScoped<ICategoryRepository,CategoryRepo>();
// builder.Services.AddScoped<ICategoryService,CategoryService>();
// builder.Services.AddScoped<IProductRepository,ProductRepo>();
// builder.Services.AddScoped<IProductService,ProductService>();
// builder.Services.AddScoped<IOrderRepository,OrderRepo>(); // need to implement address repo
// builder.Services.AddScoped<IOrderService,OrderService>();
// builder.Services.AddScoped<IReviewRepository,ReviewRepo>();
// builder.Services.AddScoped<IReviewService,ReviewService>();


// regist authroization handler (alia)
// builder.Services.AddSingleton<IAuthorizationHandler, VerifyResourceOwnerHandler>();


// Add authentication instructions
// builder
//     .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(builder.Configuration["Secrets:JwtKey"])
//             ),
//             ValidateIssuer = true,
//             ValidateAudience = false,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["Secrets:Issuer"]
//         };
//     });

// Add authentication instructions (alia)


// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// .AddJwtBearer(
//     options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Secrets:JwtKey"])),
//             ValidateIssuer = true,
//             ValidateAudience = false,
//             ValidateLifetime = true, // amke sure it's not expired
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["Secrets:Issuer"]
//         };
//     }
// );

// builder.Services.AddAuthorization(
//     policy => 
//     {
//         policy.AddPolicy("ResourceOwner", policy => policy.Requirements.Add(new VerifyResourceOwnerRequirement()));
//         policy.AddPolicy("GoldenMemberOnly", policy => policy.RequireClaim("Membership", "Golden"));
//     }
// );

builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseRouting();

// app.UseAuthentication(); // extract auth header and validate it
// app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
