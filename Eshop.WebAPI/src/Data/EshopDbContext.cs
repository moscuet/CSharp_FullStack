using System.Data.OleDb;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.ValueObject;
// using Eshop.WebApi.src.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Eshop.WebApi.src.Data
{
    public class EshopDbContext : DbContext
    {
        protected readonly IConfiguration _config;
    

        public DbSet<User>? Users { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        static EshopDbContext()
        {
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public EshopDbContext(DbContextOptions options, IConfiguration config, ILoggerFactory loggerFactory) : base(options)
        {
            _config = config;
            _loggerFactory = loggerFactory;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSnakeCaseNamingConvention();
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
                //  entity.Property(e => e.Salt).IsRequired();
                 entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                 entity.Property(e => e.Avatar).HasMaxLength(2048).HasDefaultValue(AppConstants.AVATAR_DEFAULT_IMAGE);
        
             });
        }
    }
}




    // public DbSet<Category> Categories { get; set; }
        // public DbSet<Product> Products { get; set; }
        // public DbSet<Order> Orders { get; set; }
        // public DbSet<Review> Reviews { get; set; }



        //      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder
        //     // .AddInterceptors(new TimestampInterceptor())
        //     // .EnableSensitiveDataLogging()
        //     // .EnableDetailedErrors()
        //     .UseSnakeCaseNamingConvention();
        // }



       /// OleDbPermission mine: 
             // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseNpgsql(configuration.GetConnectionString("PgDbConnection")).UseSnakeCaseNamingConvention();
        //     // optionsBuilder.UseNpgsql(configuration.GetConnectionString("PgDbConnection")).UseSnakeCaseNamingConvention().AddInterceptors(new SqlLoggingInterceptor(_loggerFactory.CreateLogger<SqlLoggingInterceptor>()));

        // }


                //  entity.Property(e => e.Role)
                //              .IsRequired()
                //              .HasDefaultValue(UserRole.User)
                //              .HasConversion<string>()
                //              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);