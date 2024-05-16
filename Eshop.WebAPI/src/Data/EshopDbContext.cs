using System.Data.OleDb;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.ValueObject;
// using Eshop.WebApi.src.Data.Interceptors;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.HasPostgresExtension("uuid-ossp");
            // craete enum table
            modelBuilder.HasPostgresEnum<UserRole>();
            modelBuilder.HasPostgresEnum<OrderStatus>();
            modelBuilder.HasPostgresEnum<SortBy>();
            modelBuilder.HasPostgresEnum<SortOrder>();

            modelBuilder.Entity<User>(entity =>
                {
                    entity.Property(e => e.UserRole)
                              .HasConversion<string>();
                    entity.HasIndex(e => e.Email).IsUnique();
                    entity.HasIndex(e => e.UserRole);
                });
        }
    }
}
