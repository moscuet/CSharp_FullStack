using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.ValueObject;
// using Eshop.WebApi.src.Data.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Data
{
    public class EshopDbContext : DbContext
    {
        //this is just a inject configuration so we can get connection string in appasettings.json
        protected readonly IConfiguration configuration;
        // public DbSet<Category> Categories { get; set; }
        // public DbSet<Product> Products { get; set; }
        // public DbSet<Order> Orders { get; set; }
        // public DbSet<Review> Reviews { get; set; }

        public DbSet<User>? Users { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public EshopDbContext(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PgDbConnection")).UseSnakeCaseNamingConvention();
                        // optionsBuilder.UseNpgsql(configuration.GetConnectionString("PgDbConnection")).UseSnakeCaseNamingConvention().AddInterceptors(new SqlLoggingInterceptor(_loggerFactory.CreateLogger<SqlLoggingInterceptor>()));

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // craete enum table
            modelBuilder.HasPostgresEnum<UserRole>();
            modelBuilder.HasPostgresEnum<OrderStatus>();
            modelBuilder.HasPostgresEnum<SortBy>();
            modelBuilder.HasPostgresEnum<SortOrder>();

            // add constrain for database between tables as we cant do it using notation
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100).HasColumnType("varchar");
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Salt).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Role).IsRequired().HasDefaultValue(UserRole.User);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Avatar).IsRequired().HasMaxLength(2048).HasDefaultValue(AppConstants.AVATAR_DEFAULT_IMAGE);
            });
        }
    }
}