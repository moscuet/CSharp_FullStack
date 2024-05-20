using System.ComponentModel.DataAnnotations;
using Eshop.Core.src.Entity;
using Eshop.Core.src.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Data
{
    public class EshopDbContext : DbContext
    {
        protected readonly IConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;

        public DbSet<User>? Users { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<Image>? Images { get; set; }
        public DbSet<Review>? Reviews { get; set; }
        public DbSet<ProductLine>? ProductLines { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<ProductColor>? ProductColors { get; set; }
        public DbSet<ProductSize>? ProductSizes { get; set; }




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
            // .AddInterceptors(new TimeStampInterceptor())
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
            modelBuilder.HasPostgresEnum<TokenType>();
            modelBuilder.HasPostgresEnum<ColorValue>();
            modelBuilder.HasPostgresEnum<EntityType>();


            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100).IsUnicode(false);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Password).HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Avatar).HasMaxLength(2048);
                entity.Property(e => e.UserRole).HasConversion<string>();
                entity.HasIndex(e => e.UserRole);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(128);
                entity.Property(e => e.ImageUrl).IsRequired().IsUnicode(false);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.ParentCategoryId).IsRequired(false);
                entity.HasOne(e => e.ParentCategory)
                      .WithMany(e => e.SubCategories)
                      .HasForeignKey(e => e.ParentCategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.ProductLines)
                      .WithOne(pl => pl.Category)
                      .HasForeignKey(pl => pl.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Address>(entity =>
                {
                    entity.Property(e => e.Street).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.House).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.City).IsRequired().HasMaxLength(50);
                    entity.Property(e => e.ZipCode).IsRequired().HasMaxLength(20);
                    entity.Property(e => e.Country).IsRequired().HasMaxLength(50);
                    entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                    entity.HasOne(e => e.User)
                        .WithMany(u => u.Addresses)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);
                });


            modelBuilder.Entity<ProductLine>(entity =>
               {
                   entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                   entity.HasIndex(e => e.Title).IsUnique();
                   entity.Property(e => e.Description).IsRequired().HasMaxLength(1080);
                   entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
                   entity.HasOne(e => e.Category)
                         .WithMany(c => c.ProductLines)
                         .HasForeignKey(e => e.CategoryId)
                         .IsRequired()
                         .OnDelete(DeleteBehavior.Cascade);
                   entity.HasMany(e => e.Products)
                         .WithOne(p => p.ProductLine)
                         .HasForeignKey(p => p.ProductLineId)
                         .OnDelete(DeleteBehavior.Cascade);
               });


            modelBuilder.Entity<Product>(entity =>
                {
                    entity.Property(e => e.Inventory).IsRequired();
                    entity.HasOne(e => e.ProductLine)
                            .WithMany(p => p.Products)
                            .HasForeignKey(e => e.ProductLineId)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Cascade);
                    entity.HasOne(e => e.Size)
                            .WithMany()
                            .HasForeignKey(e => e.SizeId)
                            .OnDelete(DeleteBehavior.SetNull);
                    entity.HasOne(e => e.Color)
                            .WithMany()
                            .HasForeignKey(e => e.ColorId)
                            .OnDelete(DeleteBehavior.SetNull);
                    entity.HasMany(e => e.Images)
                            .WithOne()
                            .HasForeignKey(i => i.EntityId)
                            .OnDelete(DeleteBehavior.Cascade);
                });

            // ProductSize Configuration
            modelBuilder.Entity<ProductSize>(entity =>
                {
                    entity.Property(e => e.Value).IsRequired();
                    entity.HasMany(e => e.Products)
                          .WithOne(p => p.Size)
                          .HasForeignKey(p => p.SizeId);
                });

            // ProductColor Configuration
            modelBuilder.Entity<ProductColor>(entity =>
              {
                  entity.HasMany(e => e.Products)
                        .WithOne(p => p.Color)
                        .HasForeignKey(p => p.ColorId);
              });

            // Review Configuration
            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.Comment).IsRequired().HasMaxLength(1080);
                entity.Property(e => e.Rating).IsRequired();
                entity.Property(e => e.IsAnonymous).IsRequired();
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Product)
                      .WithMany()
                      .HasForeignKey(e => e.ProductId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.Images)
                      .WithOne()
                      .HasForeignKey(i => i.EntityId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            // Image Configuration
            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Url).IsRequired().HasMaxLength(2048);

                // Discriminator configuration
                entity.HasDiscriminator<EntityType>("EntityType")
                      .HasValue<Image>(EntityType.Product)
                      .HasValue<Image>(EntityType.Review);

                // Configure relationships
                entity.HasOne<Product>()
                      .WithMany(p => p.Images)
                      .HasForeignKey(i => i.EntityId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);

                entity.HasOne<Review>()
                      .WithMany(r => r.Images)
                      .HasForeignKey(i => i.EntityId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);
            });

            // Order Configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Total).IsRequired().HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Address)
                      .WithMany()
                      .HasForeignKey(e => e.AddressId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.Items)
                      .WithOne(i => i.Order)
                      .HasForeignKey(i => i.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
            });



            // OrderItem Configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.Quantity)
                      .IsRequired()
                      .HasColumnType("int");

                entity.Property(e => e.Price)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Order)
                      .WithMany(o => o.Items)
                      .HasForeignKey(e => e.OrderId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                      .WithMany()
                      .HasForeignKey(e => e.ProductId);
            });

            // OrderItem Configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.Quantity)
                      .IsRequired()
                      .HasColumnType("int");

                entity.Property(e => e.Price)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Order)
                      .WithMany(o => o.Items)
                      .HasForeignKey(e => e.OrderId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                      .WithMany()
                      .HasForeignKey(e => e.ProductId);
            });

            // ProductSize Configuration
            modelBuilder.Entity<ProductSize>(entity =>
            {
                entity.Property(e => e.Value)
              .IsRequired()
              .HasConversion(v => v.ToLower(), v => v)
              .IsUnicode(false);
                entity.HasIndex(e => e.Value).IsUnique();
            });

            // ProductColor Configuration
            modelBuilder.Entity<ProductColor>(entity =>
            {
                entity.Property(e => e.Value)
              .IsRequired()
              .HasConversion(v => v.ToLower(), v => v)
              .IsUnicode(false);
                entity.HasIndex(e => e.Value).IsUnique();
            });
        }


        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added || e.State == EntityState.Modified
                           select e.Entity;

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }

            return base.SaveChanges();
        }

        public async Task InitializeDatabaseAsync()
        {
            var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "src/Data/SqlScripts", "review_functions.sql");
            if (File.Exists(scriptPath))
            {
                var script = await File.ReadAllTextAsync(scriptPath);
                await Database.ExecuteSqlRawAsync(script);
            }
            else
            {
                Console.Error.WriteLine("Failed to find SQL script for initializing database functions.");
            }
        }

    }
}
